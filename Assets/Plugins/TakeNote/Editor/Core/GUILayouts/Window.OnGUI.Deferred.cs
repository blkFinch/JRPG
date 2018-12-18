using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	internal partial class Window
	{
		private void OnGUIDeferred(Event e)
		{
			using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(_scrollVector))
			{
				_scrollVector = scrollViewScope.scrollPosition;

				foreach (var task in Ledger.Manifest.DeferredTasks)
				{
					task.Draw(e);
				}
			}
		}
	}
}