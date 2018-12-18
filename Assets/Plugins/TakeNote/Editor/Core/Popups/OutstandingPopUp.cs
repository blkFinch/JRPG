using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	public class OutstandingPopUp : PopupWindowContent
	{
		private readonly Task _task;

		public OutstandingPopUp(Task task)
		{
			_task = task;
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(160, 104);
		}

		public override void OnGUI(Rect rect)
		{
			EditorGUI.DrawRect(new Rect(0, 0, editorWindow.position.width, editorWindow.position.height), Style.PopColor);
			if (GUILayout.Button("Completed", GUILayout.Height(22)))
			{
				Scribe.MoveToCompleted(_task, ref Ledger.Manifest.OutstandingTasks);
				editorWindow.Close();
			}

			if (GUILayout.Button("Remove", GUILayout.Height(22)))
			{
				Scribe.RemoveTask(_task, ref Ledger.Manifest.OutstandingTasks);
				editorWindow.Close();
			}

			if (GUILayout.Button("Defer", GUILayout.Height(22)))
			{
				Scribe.DeferTask(_task, ref Ledger.Manifest.OutstandingTasks);
				editorWindow.Close();
			}

			if (GUILayout.Button("Cancel", GUILayout.Height(22)))
			{
				editorWindow.Close();
			}

			GUI.SetNextControlName("ClearFix");
			GUILayout.Space(20);
			EditorGUILayout.TextField("", GUIStyle.none);
			ClearFix();
		}


		private static void ClearFix()
		{
			EditorGUI.FocusTextInControl("ClearFix");
		}
	}
}