using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
    public static class SoBuilder<T> where T : ScriptableObject
    {
        public static ScriptableObject GetScriptableObject(string directory = null)
        {
            if (directory == null) { directory = Application.dataPath; }

            if (CheckPath(directory))
            {
                var settings = AssetDatabase.LoadAssetAtPath<ScriptableObject>(GetPath(directory));
                return settings;
            }

            Save(directory);
            return GetScriptableObject(directory);
        }


        internal static void Save(string directory)
        {
            var asset = ScriptableObject.CreateInstance<T>();
            var path = GetPath(directory);
            Create(asset, path);

            //TODO Asset Overwrite  logic
        }

        private static bool CheckPath(string directory)
        {
            var path = GetPath(directory);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        private static string GetPath(string directory)
        {
            var path = PathFinder.Find(directory, typeof(T).Name + ".asset");
            return path;
        }

        private static void Create(T asset, string path)
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}