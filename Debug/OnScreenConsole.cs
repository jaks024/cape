using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects.UI;
using Framework.Objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
		public OnScreenConsole(string layer, SpriteFont font, Vector2 dimension, bool isBackgroundEnabled) 
			:base(layer, font, dimension, isBackgroundEnabled)
		{
			pastConsoleEntries = new Dictionary<string, ConsoleEntry>();

			Name = "Debug Console";
		}

		public Textbox NewMessage(string message)
		{
			if (!pastConsoleEntries.TryGetValue(message, out ConsoleEntry entry))
			{
				Textbox tb = new Textbox(string.Empty, Layer, message, Font, TextColor);
				tb.Position = new Vector2(tb.Position.X, currentLine * lineSpacing);
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
