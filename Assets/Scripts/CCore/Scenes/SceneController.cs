using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CCore.Scenes
{
    public static class SceneController
    {        
        /// <summary>
        /// Open a scene found at given path. Set scene active is disabled by default.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="openSceneMode"></param>
        /// <param name="setSceneActive"></param>
        /// <returns>Scene</returns>
        public static Scene OpenSceneInEditor(
            string path, OpenSceneMode openSceneMode = OpenSceneMode.Single, bool setSceneActive = false)
        {
            Scene scene = EditorSceneManager.OpenScene(path, openSceneMode);

            if (setSceneActive)
            {
                EditorSceneManager.SetActiveScene(scene);
            }

            return scene;
        }

        /// <summary>
        /// Close scene based on name. Remove scene is true by default.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="removeScene"></param>
        /// <returns>Scene</returns>
        public static bool CloseSceneInEditor(string name, bool removeScene = true)
        {
            Scene scene = EditorSceneManager.GetSceneByName(name);

            return EditorSceneManager.CloseScene(scene, removeScene);
        }

        public static void OpenWorldEditorScene()
        {
            OpenSceneInEditor("Assets/Scenes/Editor/WorldEditor.unity");
        }

        public static void OpenGameScenes()
        {
            OpenSceneInEditor("Assets/Scenes/Game/Input.unity", OpenSceneMode.Single);

            OpenSceneInEditor("Assets/Scenes/Game/Camera.unity", OpenSceneMode.Additive);

            OpenSceneInEditor("Assets/Scenes/Game/Game.unity", OpenSceneMode.Additive, true);
        }

        /// <summary>
        /// Save all scenes and close them
        /// </summary>
        public static void CloseAllScenesInEditor()
        {
            SaveAllOpenScenesInEditor();
            
            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                Scene scene = EditorSceneManager.GetSceneAt(i);

                EditorSceneManager.CloseScene(scene, true);
            }
        }

        /// <summary>
        /// Save all scenes and close them
        /// </summary>
        public static void CloseAllScenesInEditor(params string[] filter)
        {
            SaveAllOpenScenesInEditor();
            
            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                Scene scene = EditorSceneManager.GetSceneAt(i);

                bool isSceneFiltered = false;

                for (int k = 0; k < filter.Length; k++)
                {
                    if (scene.name == filter[k])
                    {
                        isSceneFiltered = true;

                        break;
                    }
                }

                if (isSceneFiltered)
                {
                    continue;
                }

                EditorSceneManager.CloseScene(scene, true);
            }
        }

        /// <summary>
        /// Save all scenes. This is also done when closing all scenes.
        /// </summary>
        public static void SaveAllOpenScenesInEditor()
        {
            bool success = EditorSceneManager.SaveOpenScenes();

            if (!success)
            {
                EditorUtility.DisplayDialog(
                    "ERROR SAVING SCENES",
                    "There was a mishap while trying to save all open scenes in the editor :(",
                    "OK");
            }
        }
    }
}