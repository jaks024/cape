using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Objects.CellularAutomata.TestObjects
{
	public class WaterState : CellState
	{
		public enum State
		{
			Vapor = 0,
			Hot = 1,
			Warm = 2,
			Normal = 3,
			Cool = 4,
			Cold = 5,
			Solid = 6
		}

		public WaterState(State initialState)
		{
			CurrentState = (int)initialState;
		}

		public override void UpdateState(int enumIndex)
		{
			CurrentState = enumIndex;
		}
	}
}
