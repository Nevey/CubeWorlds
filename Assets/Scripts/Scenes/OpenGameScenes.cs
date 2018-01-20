using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CCore.CubeWorlds.Scenes
{
    public class OpenGameScenes
    {
        [MenuItem ("CCore/Open Game Scenes")]
        private static void Execute() 
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Features/Input.unity", OpenSceneMode.Single);

            EditorSceneManager.OpenScene("Assets/Scenes/Features/Camera.unity", OpenSceneMode.Additive);

            Scene gameScene = EditorSceneManager.OpenScene("Assets/Scenes/Features/Game.unity", OpenSceneMode.Additive);

            EditorSceneManager.SetActiveScene(gameScene);
        }
    }
}