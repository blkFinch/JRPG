using System;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

namespace FuguFirecracker.TakeNote
{
	public static class PathFinder
	{
		public static string Find(string dir, string file)
		{
			var path = Directory.GetDirectories(Environment.CurrentDirectory, dir, SearchOption.AllDirectories);
			return path[0].Replace(Environment.CurrentDirectory + Path.DirectorySeparatorChar, "") + Path.DirectorySeparatorChar + file;
		}

		public static T LoadAsset<T>(string dir, string file) where T : Object
		{
			return AssetDatabase.LoadAssetAtPath<T>(Find(dir, file));
		}
	}
}