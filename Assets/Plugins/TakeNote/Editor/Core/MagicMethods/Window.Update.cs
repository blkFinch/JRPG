namespace FuguFirecracker.TakeNote
{
	internal partial class Window
	{
		protected void Update()
		{
			if (Notice.DoFlash){Notice.Tick();}
		}
	}
}