using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Objects.CellularAutomata
{
	public abstract class CellState
	{
		public int CurrentState { get; protected set; }

		public abstract void UpdateState(int enumIndex);
	}
}
