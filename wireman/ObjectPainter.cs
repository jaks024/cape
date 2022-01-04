using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace wireman
{
	public class ObjectPainter
	{
		private Color cellColor = Color.Aqua;
		private const int cellSize = 10;
		private Dictionary<Vector2, ulong> paintedSquareDictionary;


		public ObjectPainter()
		{
			paintedSquareDictionary = new Dictionary<Vector2, ulong>();
		}

		private Vector2 GetRoundedPosition(Vector2 mousePosition)
		{
			int xInt = (int)Math.Round(mousePosition.X / (double)cellSize) * cellSize;
			int yInt = (int)Math.Round(mousePosition.Y / (double)cellSize) * cellSize;
			return new Vector2(xInt, yInt);
		}

		public void Paint(Vector2 mousePosition, GraphicsDevice gd, string layer)
		{
			Vector2 roundedPos = GetRoundedPosition(mousePosition);
			if (roundedPos.X % cellSize == 0 && roundedPos.Y % cellSize == 0)
			{
				if (!paintedSquareDictionary.ContainsKey(roundedPos))
				{
					Square sq = new Square($"cell {roundedPos.X},{roundedPos.Y}", layer, gd, cellSize, cellColor, roundedPos);
					paintedSquareDictionary.Add(roundedPos, GlobalObjectManager.Add(sq));
					
				} 
			}
		}

		public void Erase(Vector2 mousePosition, string layer)
		{
			if (paintedSquareDictionary.TryGetValue(GetRoundedPosition(mousePosition), out ulong id))
			{
				GlobalObjectManager.Remove(id);
			}
		}

		public void Clear()
		{
			foreach (ulong id in paintedSquareDictionary.Values)
			{
				GlobalObjectManager.Remove(id);
			}
			paintedSquareDictionary.Clear();
			Logger.Print("cleared paint");
		}
	}
}
