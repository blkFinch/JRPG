namespace FuguFirecracker.TakeNote
{
	internal partial class Window
	{
		internal void OnEnable()
		{
			Main.Window = this;
			Ledger.Manifest = SoBuilder<Ledger>.GetScriptableObject(Folder.Persistence) as Ledger;

			SetEnumeratedLabel(TaskCollection.Outstanding);
			SetEnumeratedLabel(TaskCollection.Completed);
			SetEnumeratedLabel(TaskCollection.Deferred);
		}
	}
}