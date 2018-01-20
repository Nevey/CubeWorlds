using UnityEditor;

namespace CCore
{
    public static class AssetHelper
    {
        public static string GetAssetPath(string parentFolder, string newFolder, string assetName, string assetExtension)
        {
            string assetPath = string.Format("{0}/{1}/{2}.{3}", parentFolder, newFolder, assetName, assetExtension);

            return assetPath;
        }

        public static bool IsValidFolder(string parentFolder, string newFolder)
        {
            return AssetDatabase.IsValidFolder(string.Format("{0}/{1}", parentFolder, newFolder));
        }

        public static void TryCreateFolder(string parentFolder, string newFolder)
        {
            if (!IsValidFolder(parentFolder, newFolder))
            {
                AssetDatabase.CreateFolder(parentFolder, newFolder);
            }
        }

        public static bool DoesAssetExist<T>(string assetPath)
        {
            return AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
        }
    }
}