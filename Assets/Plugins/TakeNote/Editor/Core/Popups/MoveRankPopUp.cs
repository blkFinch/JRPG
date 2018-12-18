using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	public class ShiftRankPopUp : PopupWindowContent
	{
		private readonly Task _task;
		private readonly int _extent;
		

		public ShiftRankPopUp(Task task)
		{
			_task = task;
			_extent = Ledger.Manifest.OutstandingTasks.Length - 1;
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(160, 46);
		}

		public override void OnClose()
		{
			Ledger.Manifest.Save();
		}


		public override void OnGUI(Rect rect)
		{
			var rank = ArrayUtility.IndexOf(Ledger.Manifest.OutstandingTasks, _task);
			EditorGUI.DrawRect(new Rect(0, 0, editorWindow.position.width, editorWindow.position.height), Style.PopColor);

			using (new EditorGUI.DisabledScope(rank == 0))
			{
				if (GUILayout.Button("Move Up"))
				{
					Ledger.Manifest.TraverseRanks(_task, -1);
					Main.Window.Repaint();
				}
			}

			using (new EditorGUI.DisabledScope(rank >= _extent))
			{
				if (GUILayout.Button("Move Down"))
				{
					Ledger.Manifest.TraverseRanks(_task, 1);
					Main.Window.Repaint();
				}
			}
		}
	}
}