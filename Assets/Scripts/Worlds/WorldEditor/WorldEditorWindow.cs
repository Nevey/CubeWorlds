using UnityEngine;
using UnityEditor;
using System;
using CCore.CubeWorlds.Worlds.WorldTiles;

namespace CCore.CubeWorlds.Worlds.Editor
{
    public class WorldEditorWindow : EditorWindow
    {
        private string worldName;

        private WorldTile tilePrefab;

        private int gridSize;

        private float spaceBetweenTiles;

        private GameObject worldGameObject;

        [MenuItem("CCore/World Editor")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(WorldEditorWindow));
        }

        private void OnGUI()
        {
            minSize = new Vector2(300, 200);

            DrawTopHeader();

            DrawWorldInfo();
            
            DrawWorldSettings();

            DrawButtons();
        }

        private void DrawTopHeader()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.Label("WORLD BUILDER", EditorStyles.largeLabel);

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.Space();
        }

        private void DrawWorldInfo()
        {
            EditorGUILayout.LabelField("World Info", EditorStyles.boldLabel);

            worldName = EditorGUILayout.TextField("World Name", worldName);

            EditorGUILayout.Space();
        }

        private void DrawWorldSettings()
        {
            EditorGUILayout.LabelField("World Settings", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Height(500));

            tilePrefab = (WorldTile)EditorGUILayout.ObjectField(tilePrefab, typeof(WorldTile), false);

            gridSize = EditorGUILayout.IntSlider("World Size", gridSize, 1, WorldEditorConstants.maxGridSize);

            spaceBetweenTiles = EditorGUILayout.Slider("Space Between Tiles", spaceBetweenTiles, 0, WorldEditorConstants.maxSpaceBetweenTiles);

            EditorGUILayout.Space();
        }

        private void DrawButtons()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create", GUILayout.Width(100), GUILayout.Height(50)))
            {
                TryCreateWorld();
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Save", GUILayout.Width(100), GUILayout.Height(50)))
            {
                TrySaveWorld();
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        private void TryCreateWorld()
        {
            if (worldGameObject != null)
            {
                if (EditorUtility.DisplayDialog(
                    "Destroy current world?",
                    "If you want to create a new world, the old world will be destroyed. Do you wish to continue?",
                    "Yep!",
                    "Shit no! Take me back!"))
                {
                    DestroyImmediate(worldGameObject);

                    CreateWorld();
                }
            }
            else
            {
                CreateWorld();
            }
        }

        private void CreateWorld()
        {
            worldGameObject = new GameObject();

            worldGameObject.name = worldName;

            worldGameObject.AddComponent<WorldBuilder>().CreateWorldGrid(gridSize, tilePrefab, spaceBetweenTiles);
        }

        private void TrySaveWorld()
        {
            string prefabPath = String.Format("Assets/Prefabs/Worlds/{0}.prefab", worldName);

            if (AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)))
            {
                if (EditorUtility.DisplayDialog(
                    "Prefab already exists!",
                    "Do you wish to overwrite prefab with name: " + worldName + "?",
                    "Yep!",
                    "Shit no! Take me back!"))
                {
                    CreateWorldPrefab(prefabPath);
                }
            }
            else
            {
                CreateWorldPrefab(prefabPath);
            }
        }

        private void CreateWorldPrefab(string prefabPath)
        {
            // TODO: Add components:
            // - World.cs
            // - WorldCameraEnabler.cs
            // - WorldPlayerSpawner.cs
            
            UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab(prefabPath);

            PrefabUtility.ReplacePrefab(worldGameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
        }
    }
}