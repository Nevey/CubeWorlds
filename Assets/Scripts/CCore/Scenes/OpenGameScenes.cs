using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CCore.Scenes
{
    public class OpenGameScenes
    {
        [MenuItem("CCore/Open Game Scenes")]
        private static void Execute() 
        {
            SceneController.OpenGameScenes();
        }
    }
}