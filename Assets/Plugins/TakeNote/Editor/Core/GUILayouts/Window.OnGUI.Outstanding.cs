using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	internal partial class Window
	{
		private void OnGUIOutstanding(Event e)
		{
			using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(_scrollVector))
			{
				_scrollVector = scrollViewScope.scrollPosition;

				foreach (var task in Ledger.Manifest.OutstandingTasks)
				{
					task.Draw(e);
				}
			}

			GUI.backgroundColor = Style.DimColor;
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			GUI.backgroundColor = Style.ResetColor;
			GUILayout.Space(6);

			if (!DoAdd)
			{
				DoAdd = GUILayout.Toggle(DoAdd, Ikon.More, "Button", GUILayout.Height(26));
			}

			if (DoAdd)
			{
				EditorGUILayout.LabelField("Task*");
				GUI.backgroundColor = TaskAlertColor;
				TaskString = EditorGUILayout.TextField(string.Empty, TaskString);
				GUI.backgroundColor = Style.ResetColor;

				GUILayout.Space(8);

				EditorGUILayout.BeginHorizontal("Button");
				DoDetails = GUILayout.Toggle(DoDetails, "Add Details", Style.AlignCenter, GUILayout.Height(24));
				DoDetails = GUILayout.Toggle(DoDetails, string.Empty, Style.OnOffSwitch);
				EditorGUILayout.EndHorizontal();


				if (DoDetails)
				{
					DetailsString = EditorGUILayout.TextArea(DetailsString, Style.WordWrap, GUILayout.Height(58));
				}

				EditorGUILayout.BeginHorizontal("Button");
				DoColor = GUILayout.Toggle(DoColor, "Colorize", Style.AlignCenter, GUILayout.Height(24));
				DoColor = GUILayout.Toggle(DoColor, string.Empty, Style.OnOffSwitch);
				EditorGUILayout.EndHorizontal();


				if (DoColor)
				{
					ColorizeColor = EditorGUILayout.ColorField(ColorizeColor);
				
				}

				GUILayout.Space(8);

				EditorGUILayout.BeginHorizontal();

				if (GUILayout.Button(Ikon.Add, GUILayout.Height(26)))
				{
				
					Scribe.WriteTask();
				}

				if (GUILayout.Button(Ikon.Check, GUILayout.Height(26)))
				{
					DoAdd = !Scribe.WriteTask();
				}

				if (GUILayout.Button(Ikon.Close, GUILayout.Height(26)))
				{
					DoAdd = false;
				}

				EditorGUILayout.EndHorizontal();
			}

			GUILayout.Space(6);
			EditorGUILayout.EndVertical();

			GUILayout.FlexibleSpace();
		}
	}
}