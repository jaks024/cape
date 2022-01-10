using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects;
using Framework.Managers;
using Framework.Debug;

namespace Framework.Utilities
{
	public class ObjectPainter
	{
		private Color cellColor = Color.Aqua;
		private const int cellSize = 10;
		private Dictionary<Vector2, ulong> paintedSquareDictionary;
		private GameObjectManager objectManager;

		public ObjectPainter(GameObjectManager globalObjectManager)
		{
			objectManager = globalObjectManager;
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
					paintedSquareDictionary.Add(roundedPos, objectManager.Add(sq));
					Logger.PrintToUI("added {0} to layer {1}", sq.Name, sq.Layer);
				} 
			}
		}

		public void Erase(Vector2 mousePosition, string layer)
		{
			if (paintedSquareDictionary.TryGetValue(GetRoundedPosition(mousePosition), out ulong id))
			{
				objectManager.Remove(id);
			}
		}

		public void Clear()
		{
			foreach (ulong id in paintedSquareDictionary.Values)
			{
				objectManager.Remove(id);
			}
			paintedSquareDictionary.Clear();
			Logger.PrintToUI("cleared paint");
		}
	}
}
