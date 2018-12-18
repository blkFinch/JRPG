using System.Collections;
using UnityEngine;

namespace FuguFirecracker.TakeNote
{
	public class Notice
	{
		private const float HANG_TIME = 0.04f;
		private const int TRANSITION_STEPS = 10;

		private static float _tickTime;
		private static IEnumerator _enumerator;

		internal static bool DoFlash { get; private set; }

		internal static void Tick()
		{
		
			if (Time.realtimeSinceStartup - _tickTime >= HANG_TIME)
			{
				_enumerator.MoveNext();
			}
		}

		internal static void Flash(Color flashColor)
		{
			DoFlash = true;
			_enumerator = FlashColor(flashColor);
		}

		private static IEnumerator FlashColor(Color flashColor)
		{
			_tickTime = Time.realtimeSinceStartup;

			Color c;
			var steps = TRANSITION_STEPS;
			var backwards = steps;

			var r1 = Style.ResetColor.r;
			var g1 = Style.ResetColor.g;
			var b1 = Style.ResetColor.b;
			var a1 = Style.ResetColor.a;

			var r2 = flashColor.r;
			var g2 = flashColor.g;
			var b2 = flashColor.b;
			var a2 = flashColor.a;

			var rDif = r1 < r2 ? Mathf.Abs(r1 - r2) / steps : (Mathf.Abs(r1 - r2) / steps) * -1;
			var gDif = g1 < g2 ? Mathf.Abs(g1 - g2) / steps : (Mathf.Abs(g1 - g2) / steps) * -1;
			var bDif = b1 < b2 ? Mathf.Abs(b1 - b2) / steps : (Mathf.Abs(b1 - b2) / steps) * -1;
			var aDif = a1 < a2 ? Mathf.Abs(a1 - a2) / steps : (Mathf.Abs(a1 - a2) / steps) * -1;

			while (steps > 0)
			{
				r1 += rDif;
				g1 += gDif;
				b1 += bDif;
				a1 += aDif;

				c = new Color(r1, g1, b1, a1);
				Main.Window.TaskAlertColor = c;
				Main.Window.Repaint();
				steps--;
				_tickTime = Time.realtimeSinceStartup;
				yield return null;
			}

			var wait = 7f;

			while (wait > 0)
			{
				wait -= Time.deltaTime;
				yield return null;
			}

			while (backwards > 0)
			{
				r1 -= rDif;
				g1 -= gDif;
				b1 -= bDif;
				a1 -= aDif;

				c = new Color(r1, g1, b1, a1);
				Main.Window.TaskAlertColor = c;
				Main.Window.Repaint();
				backwards--;
				_tickTime = Time.realtimeSinceStartup;
				yield return null;
			}

			DoFlash = false;
		}
	}
}