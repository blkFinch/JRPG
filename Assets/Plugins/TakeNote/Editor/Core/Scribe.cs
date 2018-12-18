using System;
using System.Linq;

namespace FuguFirecracker.TakeNote
{
	internal class Scribe
	{
		internal static bool WriteTask()
		{
			if (string.IsNullOrEmpty(Main.Window.TaskString))
			{
				Notice.Flash(Style.FlashAlertColor);
				return false;
			}

			Main.Window.TaskString = Main.Window.TaskString.Trim();
			if (string.IsNullOrEmpty(Main.Window.TaskString))
			{
				Notice.Flash(Style.FlashAlertColor);
				return false;
			}

			if (!string.IsNullOrEmpty(Main.Window.DetailsString))
			{
				Main.Window.DetailsString = Main.Window.DetailsString.Trim();
			}


			Main.Window.DoDetails = !string.IsNullOrEmpty(Main.Window.DetailsString);

			var task = new Task
			{
				Title = Main.Window.TaskString,
				CreationDate = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString(),
				DrawColor = Main.Window.ColorizeColor,
				HasDetails = Main.Window.DoDetails,
				Details = Main.Window.DetailsString,
				IsColored = Main.Window.DoColor,
			};

			Main.Window.TaskString = string.Empty;
			Main.Window.DetailsString = string.Empty;

			var taskList = Ledger.Manifest.OutstandingTasks.ToList();
			taskList.Add(task);
			Ledger.Manifest.OutstandingTasks = taskList.ToArray();

			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Outstanding);

			Ledger.Manifest.Save();
			return true;
		}

		internal static void DeferTask(Task task, ref Task[] tasks)
		{
			task.DoShowMore = false;
			task.IsCompleted = false;
			task.CompletionDate = string.Empty;
			task.IsDeferred = true;
		
			var taskList = tasks.ToList();
			var deferredList = Ledger.Manifest.DeferredTasks.ToList();

			deferredList.Add(task);
			taskList.Remove(task);

			tasks = taskList.ToArray();
			Ledger.Manifest.DeferredTasks = deferredList.ToArray();
		
			Ledger.Manifest.Save();	

			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Completed);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Outstanding);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Deferred);
		}

		internal static void RemoveTask(Task task, ref Task[] tasks)
		{
			var taskList = tasks.ToList();
			taskList.Remove(task);
			tasks = taskList.ToArray();

			Ledger.Manifest.Save();
			
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Completed);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Outstanding);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Deferred);

		}

		internal static void MoveToCompleted(Task task, ref Task[] tasks)
		{
			task.DoShowMore = false;
			task.IsCompleted = true;
			task.IsDeferred = false;
			task.CompletionDate = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString();

			var taskList = tasks.ToList();
			var completedList = Ledger.Manifest.CompletedTasks.ToList();

			completedList.Add(task);
			taskList.Remove(task);

			tasks = taskList.ToArray();
			Ledger.Manifest.CompletedTasks = completedList.ToArray();
			Ledger.Manifest.Save();

			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Outstanding);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Completed);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Deferred);

		}

		internal static  void MoveToOutstanding(Task task, ref Task[] tasks)
		{
			task.DoShowMore = false;
			task.IsCompleted = false;
			task.IsDeferred = false;
			task.CompletionDate = string.Empty;

			var outstandingTasks = Ledger.Manifest.OutstandingTasks.ToList();
			var taskList = tasks.ToList();

			taskList.Remove(task);
			outstandingTasks.Add(task);

			Ledger.Manifest.OutstandingTasks = outstandingTasks.ToArray();
			tasks = taskList.ToArray();
			Ledger.Manifest.Save();

			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Outstanding);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Completed);
			Main.Window.SetEnumeratedLabel(Window.TaskCollection.Deferred);
		}
	}
}