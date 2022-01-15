using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Objects.CellularAutomata
{
	public abstract class CellRule
	{
		public abstract bool ReadyToEvaluate();
		public abstract void EvaluateRule();
	}
}
