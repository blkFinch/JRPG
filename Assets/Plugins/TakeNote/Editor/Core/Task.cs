using System;
using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	[Serializable]
	public class Task
	{
	
		public string Title;
		public string CreationDate;
		public string CompletionDate;
		public bool HasDetails;
		public string Details;
		public bool IsCompleted;
		public bool IsDeferred;
		public bool IsColored;
		public Color DrawColor;

		public bool DoShowMore { get; set; }

		private Rect _clickOnRect;

		internal void Draw(Event e)
		{
			if (IsColored)
			{
				GUI.backgroundColor = DrawColor;
			}

			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Title, Style.AlignBold);

			DoShowMore = GUILayout.Toggle(DoShowMore, Ikon.More, Style.ZButton);

			EditorGUILayout.EndHorizontal();

			if (DoShowMore)
			{
				if (HasDetails)
				{
					EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2, Style.EditorLine), Style.SeperatorColor);
					EditorGUILayout.LabelField(Details, EditorStyles.wordWrappedLabel);
				}

				EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2, Style.EditorLine), Style.SeperatorColor);

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.BeginVertical();
				EditorGUILayout.LabelField("Created on :     " + CreationDate, EditorStyles.miniLabel);
				if (IsCompleted)
				{
					EditorGUILayout.LabelField("Completed on : " + CompletionDate, EditorStyles.miniLabel);
				}

				EditorGUILayout.EndVertical();

				if (GUILayout.Button(Ikon.Edit, Style.ZButton))
				{
					PopupWindow.Show(_clickOnRect, new EditTaskPopUp(this));
				}

				if (e.type == EventType.Repaint)
				{
					_clickOnRect = new Rect(10, GUILayoutUtility.GetLastRect().y, 0, 0);
				}
				
				if (!IsCompleted && !IsDeferred)
				{
					if (GUILayout.Button(Ikon.UpDown, Style.ZButton))
					{
						PopupWindow.Show(new Rect(Main.Window.position.width - 220, _clickOnRect.y, 0, 0), new ShiftRankPopUp(this));

					}  
				}

				if (IsCompleted)
				{
					if (GUILayout.Button(Ikon.Trash, Style.ZButton))
					{
						PopupWindow.Show(new Rect(Main.Window.position.width - 190, _clickOnRect.y, 0, 0), new CompletedPopUp(this));
					}
				}
				else if (IsDeferred)
				{
					if (GUILayout.Button(Ikon.Trash, Style.ZButton))
					{
						PopupWindow.Show(new Rect(Main.Window.position.width - 190, _clickOnRect.y, 0, 0), new DeferredPopUp(this));
					}
				}
				else
				{
					if (GUILayout.Button(Ikon.Check, Style.ZButton))
					{
						PopupWindow.Show(new Rect(Main.Window.position.width - 190, _clickOnRect.y, 0, 0), new OutstandingPopUp(this));
					}
				}
					

				EditorGUILayout.EndHorizontal();
			}

			GUILayout.Space(4);
			EditorGUILayout.EndVertical();
			GUI.backgroundColor = Style.ResetColor;
		}

		public Task Clone()
		{
			return new Task
			{
			
				Title = Title,
				CreationDate = CreationDate,
				CompletionDate = CompletionDate,
				HasDetails = HasDetails,
				Details = Details,
				IsCompleted = IsCompleted,
				IsColored = IsColored,
				DrawColor = DrawColor
			};
		}

		public void Assimilate(Task task)
		{
		
			task.Title = Title;
			task.CreationDate = CreationDate;
			task.CompletionDate = CompletionDate;
			task.HasDetails = HasDetails;
			task.Details = Details;
			task.IsCompleted = IsCompleted;
			task.IsColored = IsColored;
			task.DrawColor = DrawColor;
		}
	}
}