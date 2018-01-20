using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CCore.Scenes
{
    public class OpenGameScenes
    {
        [MenuItem ("CCore/Open Game Scenes")]
        private static void Execute() 
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Game/Input.unity", OpenSceneMode.Single);

            EditorSceneManager.OpenScene("Assets/Scenes/Game/Camera.unity", OpenSceneMode.Additive);

            Scene gameScene = EditorSceneManager.OpenScene("Assets/Scenes/Game/Game.unity", OpenSceneMode.Additive);

            EditorSceneManager.SetActiveScene(gameScene);
        }
    }
}