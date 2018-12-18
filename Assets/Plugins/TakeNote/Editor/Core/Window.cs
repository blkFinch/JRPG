using UnityEditor;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	internal partial class Window : EditorWindow
	{
		#region UIFields

		internal string TaskString;
		internal string DetailsString;
		internal bool DoAdd;
		internal bool DoDetails;
		internal bool DoColor;
		internal Color ColorizeColor = Color.white;

		internal Color TaskAlertColor = Color.white;
		private Vector2 _scrollVector;

		#endregion


		[MenuItem("Tools/Fugu Firecracker/Take Note")]
		public static void OpenWindow()
		{
			GetWindow<Window>("Take Note");
		}
	}
}