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
					Color c = new Color(roundedPos.X / 1000, roundedPos.Y / 1000, (roundedPos.X + roundedPos.Y)/ 2000);
					Square sq = new Square($"cell {roundedPos.X},{roundedPos.Y}", layer, cellSize, c, roundedPos);
					ulong id = objectManager.Add(sq);
					paintedSquareDictionary.Add(roundedPos, id);
					//Logger.PrintToUI("added {0} to layer {1}", sq.Name, sq.Layer);
					//Logger.DebugUIManager.AddObjectToDisplayer(id);
				} 
			}
		}

		public void Erase(Vector2 mousePosition, string layer)
		{
			if (paintedSquareDictionary.TryGetValue(GetRoundedPosition(mousePosition), out ulong id))
			{
				objectManager.Remove(id);
				Logger.DebugUIManager.RemoveObjectFromDisplayer(id);
				paintedSquareDictionary.Remove(GetRoundedPosition(mousePosition));
			}
		}

		public void Clear()
		{
			foreach (ulong id in paintedSquareDictionary.Values)
			{
				objectManager.Remove(id);
				Logger.DebugUIManager.RemoveObjectFromDisplayer(id);
			}
			paintedSquareDictionary.Clear();
			Logger.PrintToUI("cleared paint");
		}
	}
}
