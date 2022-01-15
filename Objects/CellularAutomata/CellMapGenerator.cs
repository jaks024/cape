using System;
using System.Collections.Generic;
using System.Text;
using Framework.Debug;
using Framework.Managers;
using Microsoft.Xna.Framework;

namespace Framework.Objects.CellularAutomata
{
	public class CellMapGenerator
	{

		private int updateRate;
		private int updateTimer;

		private Dictionary<Vector2, CellChunk> chunkMap;

		public CellMapGenerator(int updateRate, string mapLayer, GameObjectManager gameObjectManager)
		{
			this.updateRate = updateRate;
			chunkMap = new Dictionary<Vector2, CellChunk>();

			// TEMPORARY
			int chunkSize = 20;
			Vector2 chunkPosition = new Vector2(200, 50);
			CellChunk chunk = new CellChunk(0, chunkSize, mapLayer, 1, chunkPosition, gameObjectManager);
			chunkMap.Add(chunkPosition, chunk);

			int[] cellmap = new int[chunkSize * chunkSize];
			for (int i = 0; i < cellmap.Length; i++)
			{
				cellmap[i] = 0;
			}

			chunk.FillChunk(cellmap);
		}

		public void Update()
		{
			updateTimer++;
			if (updateTimer >= updateRate)
			{
				updateTimer = 0;
				foreach (CellChunk chunk in chunkMap.Values)
				{
					chunk.UpdateChunk();
					Logger.PrintToUI("updated chunk");
				}
			}
		}
	}
}
