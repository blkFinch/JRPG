using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	internal partial class Window
	{
		internal void OnGUI()
		{
			var e = Event.current;

			OnGUIHeader();

			switch (_taskCollection)
			{
				case TaskCollection.Outstanding:
					OnGUIOutstanding(e);
					break;
				case TaskCollection.Completed:
					OnGUIComplete(e);
					break;
				case TaskCollection.Deferred:
					OnGUIDeferred(e);
					break;
				default:
					OnGUIOutstanding(e);
					break;
			}
		}
	}
}