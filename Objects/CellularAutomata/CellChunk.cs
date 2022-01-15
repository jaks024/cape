using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Framework.Managers;
using Framework.Objects.CellularAutomata.TestObjects;
using Framework.Debug;

namespace Framework.Objects.CellularAutomata
{
	public class CellChunk
	{
		private const int cellSize = 5; // TEMPORARY
		private int chunkId;
		private int chunkSize;
		private string chunkLayer;
		
		private int cellUpdateRate;
		private int cellUpdateTimer;
		private int cellUpdateIndex;

		private GameObject chunkCellParent;
		private Cell[] chunk;
		private CellChunk[] neighbouringChunks;
		private GameObjectManager cellObjectManager;

		public CellChunk(int chunkId, int chunkSize, string chunkLayer, int cellUpdateRate,
			Vector2 chunkPosition, GameObjectManager cellObjectManager)
		{
			this.chunkId = chunkId;
			this.chunkSize = chunkSize;
			this.chunkLayer = chunkLayer;
			this.cellUpdateRate = cellUpdateRate;
			chunk = new Cell[chunkSize * chunkSize];
			this.cellObjectManager = cellObjectManager;

			chunkCellParent = new GameObject();
			chunkCellParent.Position = chunkPosition;
		}

		public void AssignNeighbours(CellChunk[] neighbours)
		{
			neighbouringChunks = neighbours;
		}

		public void FillChunk(int[] cellTypeMap)
		{
			if (cellTypeMap.Length != chunkSize * chunkSize)
			{
				return;
			}

			for(int i = 0; i < chunk.Length; ++i)
			{
				if (cellTypeMap[i] == 0)
				{
					int yPos = i / chunkSize;
					int xPos = i - yPos * chunkSize;
					Square visualObject = new Square($"c{chunkId}c-{i}", chunkLayer, cellSize, Color.White, new Vector2(xPos * cellSize, yPos * cellSize));
					chunkCellParent.AddChild(visualObject);
					Water waterCell = new Water(visualObject, new WaterState(WaterState.State.Normal));
					waterCell.AddRule(new WaterRule(waterCell));
					chunk[i] = waterCell;

					cellObjectManager.Add(visualObject);
					//Logger.DebugUIManager.AddObjectToDisplayer(cellObjectManager.Add(visualObject));
				}		
			}

			AssignCellNeighbour();
		}

		private void AssignCellNeighbour()
		{
			for (int i = 0; i < chunk.Length; i++)
			{
				int yPos = i / chunkSize;
				int xPos = i - yPos * chunkSize;
				chunk[i].AssignNeighbours(new Cell[8] {
					GetCellAt(xPos - 1, yPos - 1), GetCellAt(xPos, yPos - 1), GetCellAt(xPos + 1, yPos - 1),
					GetCellAt(xPos - 1, yPos),                                GetCellAt(xPos + 1, yPos),
					GetCellAt(xPos - 1, yPos + 1), GetCellAt(xPos, yPos + 1), GetCellAt(xPos + 1, yPos + 1),
				});
			}
		}

		private Cell GetCellAt(int x, int y)
		{
			int index = y * chunkSize + x;
			if (index >= 0 && index < chunk.Length)
			{
				return chunk[y * chunkSize + x];
			}
			
			// add cross chunk support

			return null;
		}

		public void UpdateChunk()
		{
			cellUpdateTimer++;
			if (cellUpdateTimer >= cellUpdateRate)
			{
				if (chunk[cellUpdateIndex] != null)
				{
					cellUpdateTimer = 0;

					chunk[cellUpdateIndex].Update();
					Logger.PrintToUI("updated");
				}
				cellUpdateIndex++;
				if (cellUpdateIndex >= chunk.Length)
				{
					cellUpdateIndex = 0;
				}
			}		
		}
	}
}
