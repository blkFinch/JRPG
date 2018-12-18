using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	internal class EditTaskPopUp : PopupWindowContent
	{
		private const float MAX_HEIGHT = 162;
		private const float COLOR_HEIGHT = 20;
		private const float DETAILS_HEIGHT = 60;

		private float _colorHeight;
		private float _detailsHeight;

		private readonly Task _editTask;
		private readonly Task _task;

		public EditTaskPopUp(Task task)
		{
			_task = task;
			_editTask = _task.Clone();
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(Main.Window.position.width - 42, MAX_HEIGHT + _colorHeight + _detailsHeight);
		}

		public override void OnGUI(Rect rect)
		{
			EditorGUI.DrawRect(new Rect(0, 0, editorWindow.position.width, editorWindow.position.height), Style.PopColor);
			GUILayout.Space(6);
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			GUI.backgroundColor = Color.white;

			EditorGUILayout.LabelField("Edit Task", EditorStyles.boldLabel);

			GUI.SetNextControlName("TaskString");
			_editTask.Title = EditorGUILayout.TextField(string.Empty, _editTask.Title);

			GUILayout.Space(8);

			EditorGUILayout.BeginHorizontal("Button");
			_editTask.HasDetails = GUILayout.Toggle(_editTask.HasDetails, "Add Details", Style.AlignCenter, GUILayout.Height(24));
			_editTask.HasDetails = GUILayout.Toggle(_editTask.HasDetails, string.Empty, Style.OnOffSwitch);
			EditorGUILayout.EndHorizontal();

			if (_editTask.HasDetails)
			{
				_detailsHeight = DETAILS_HEIGHT;
				_editTask.Details = EditorGUILayout.TextArea(_editTask.Details, Style.WordWrap, GUILayout.Height(58));
			}
			else
			{
				_detailsHeight = 0;
			}

			EditorGUILayout.BeginHorizontal("Button");
			_editTask.IsColored = GUILayout.Toggle(_editTask.IsColored, "Colorize", Style.AlignCenter, GUILayout.Height(24));
			_editTask.IsColored = GUILayout.Toggle(_editTask.IsColored, string.Empty, Style.OnOffSwitch);
			EditorGUILayout.EndHorizontal();

			if (_editTask.IsColored)
			{
				_colorHeight = COLOR_HEIGHT;
				_editTask.DrawColor = EditorGUILayout.ColorField(_editTask.DrawColor);
			}
			else
			{
				_colorHeight = 0;
			}

			GUILayout.Space(8);

			EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Apply", GUILayout.Height(22)))
			{
				_editTask.Assimilate(_task);
				Ledger.Manifest.Save();
 				editorWindow.Close();
			}

			if (GUILayout.Button("Cancel", GUILayout.Height(22)))
			{
				editorWindow.Close();
			}

			EditorGUILayout.EndHorizontal();
			GUILayout.Space(6);
			EditorGUILayout.EndVertical();
		}
	}
}