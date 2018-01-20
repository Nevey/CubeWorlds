using CCore.CubeWorlds.Worlds.WorldTiles;
using CCore.Scenes;
using UnityEditor;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds.Editor
{
    [ExecuteInEditMode]
    public class WorldEditorController
    {
        private const string worldEditorSceneName = "WorldEditor";

        private GameObject worldGameObject;

        private void CreateWorld(string worldName, int gridSize, WorldTile tilePrefab, float spaceBetweenTiles)
        {
            if (worldGameObject != null)
            {
                MonoBehaviour.DestroyImmediate(worldGameObject);
            }

            worldGameObject = new GameObject();

            worldGameObject.name = worldName;

            worldGameObject.AddComponent<WorldBuilder>().CreateWorldGrid(gridSize, tilePrefab, spaceBetweenTiles);
        }

        private void CreateWorldConfig(string assetPath)
        {
            WorldConfig worldConfig = ScriptableObject.CreateInstance<WorldConfig>();

            WorldBuilder worldBuilder = worldGameObject.GetComponent<WorldBuilder>();

			worldConfig.SetData(worldBuilder.GridSize);

			AssetDatabase.CreateAsset(worldConfig, assetPath);
			
			AssetDatabase.SaveAssets();
        }

        private void CreateWorldPrefab(string prefabPath)
        {
            UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab(prefabPath);

            MonoBehaviour.DestroyImmediate(worldGameObject.GetComponent<WorldBuilder>());

            worldGameObject.AddComponent<World>();
            worldGameObject.AddComponent<WorldCameraEnabler>();
            worldGameObject.AddComponent<WorldPlayerSpawner>();

            PrefabUtility.ReplacePrefab(worldGameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
        }

        private void CloseOtherScenes()
        {
            SceneController.CloseAllScenesInEditor();

            SceneController.OpenWorldEditorScene();
        }

        public void TryCreateWorld(string worldName, int gridSize, WorldTile tilePrefab, float spaceBetweenTiles)
        {
            CloseOtherScenes();

            if (worldGameObject != null)
            {
                if (!EditorUtility.DisplayDialog(
                    "Destroy current world?",
                    "If you want to create a new world, the old world will be destroyed. Do you wish to continue?",
                    "Yep!",
                    "Shit no! Take me back!"))
                {
                    return;
                }
            }

            CreateWorld(worldName, gridSize, tilePrefab, spaceBetweenTiles);
        }

        public void TrySaveWorldConfig(string worldName)
        {
            string parentFolder = "Assets/Config";

            string newFolder = "Resources";

            string extension = "asset";

            string assetPath = AssetHelper.GetAssetPath(parentFolder, newFolder, worldName, extension);

            if (AssetHelper.DoesAssetExist<WorldConfig>(assetPath))
            {
                if (!EditorUtility.DisplayDialog(
                    "Config file already exists!",
                    "Do you wish to overwrite world config with name: " + worldName + "?",
                    "Yep!",
                    "Shit no! Take me back!"))
                {
                    return;
                }
            }

            AssetHelper.TryCreateFolder(parentFolder, newFolder);

            CreateWorldConfig(assetPath);
        }

        public void TrySaveWorldPrefab(string worldName)
        {
            string parentFolder = "Assets/Prefabs";

            string newFolder = "Worlds";

            string extension = "prefab";

            string assetPath = AssetHelper.GetAssetPath(parentFolder, newFolder, worldName, extension);

            if (AssetHelper.DoesAssetExist<GameObject>(assetPath))
            {
                if (!EditorUtility.DisplayDialog(
                    "Prefab already exists!",
                    "Do you wish to overwrite prefab with name: " + worldName + "?",
                    "Yep!",
                    "Shit no! Take me back!"))
                {
                    return;
                }
            }

            AssetHelper.TryCreateFolder(parentFolder, newFolder);

            CreateWorldPrefab(assetPath);
        }
    }
}