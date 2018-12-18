using System;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	internal partial class Window
	{
		internal enum TaskCollection{Outstanding, Completed, Deferred}
		private readonly string[] _taskStatusStrings = {"Outstanding", "Completed", "Deferred"};
		private TaskCollection _taskCollection;

		private void OnGUIHeader()
		{
			GUILayout.Space(6);

			_taskCollection = (TaskCollection) GUILayout.Toolbar((int) _taskCollection, _taskStatusStrings, GUILayout.Height(26));
		}

		internal void SetEnumeratedLabel(TaskCollection taskCollection)
		{
			int count;

			switch (taskCollection)
			{
				case TaskCollection.Outstanding:
					count = Ledger.Manifest.OutstandingTasks.Length;
					break;
				case TaskCollection.Completed:
					count = Ledger.Manifest.CompletedTasks.Length;
					break;
				case TaskCollection.Deferred:
					count = Ledger.Manifest.DeferredTasks.Length;
					break;
				default:
					count = 0;
					break;
			}

			_taskStatusStrings[(int) taskCollection] = string.Format("{0} {1}", Enum.GetName(typeof(TaskCollection), taskCollection), HandleZero(count));
		}

		private static string HandleZero(int count)
		{
			return count == 0 ? string.Empty : string.Format("[{0}]", count);
		}
	}
}