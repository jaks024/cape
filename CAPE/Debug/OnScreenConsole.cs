using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects.UI;
using Framework.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace Framework.Debug
{
	public class OnScreenConsole : UIControl
	{
		private class ConsoleEntry
		{
			public int Occurences { get; set; }
			public Textbox TextBox { get; set; }

			public ConsoleEntry(int occ, Textbox tb)
			{
				Occurences = occ;
				TextBox = tb;
			}

			public string GetPrintText(string orignalText)
			{
				return $"{Occurences} | {orignalText}";
			}
		}

		private Dictionary<string, ConsoleEntry> pastConsoleEntries;
		private int currentLine = 0;
		private int lineSpacing = 20;
		public OnScreenConsole(string renderLayerName, SpriteFont textFont)
		{
			pastConsoleEntries = new Dictionary<string, ConsoleEntry>();

			Layer = renderLayerName;
			Font = textFont;
			RenderMode = Rendering.RenderMode.Skip;
			Position = new Microsoft.Xna.Framework.Vector2(-200, -200);
		}

		public Textbox NewMessage(string message)
		{
			if (!pastConsoleEntries.TryGetValue(message, out ConsoleEntry entry))
			{
				Textbox tb = new Textbox(string.Empty, Layer, message, Font);
				tb.Position = new Microsoft.Xna.Framework.Vector2(tb.Position.X, currentLine * lineSpacing);
				AddChild(tb);

				++currentLine;
				pastConsoleEntries.Add(message, new ConsoleEntry(1, tb));
				return tb;
			}
			else
			{
				++entry.Occurences;
				entry.TextBox.Text = entry.GetPrintText(message);
				return null;
			}
		}




	}
}
