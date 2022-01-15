using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Objects.CellularAutomata.TestObjects
{
	public class WaterRule : CellRule
	{
		private Water water;
		public WaterRule(Water water)
		{
			this.water = water;
		}

		public override void EvaluateRule()
		{
			Debug.Logger.PrintToUI("evaluated {0}", water.Temperature);
			water.Temperature -= 5;
			RadiateTemperature();
			water.UpdateState(TemperatureRule(water.Temperature));

		}

		public override bool ReadyToEvaluate()
		{
			return water != null;
		}

		private int TemperatureRule(float currentTemp)
		{
			if (currentTemp >= 150)
			{
				return (int)WaterState.State.Vapor;
			}
			else if (currentTemp >= 100)
			{
				return (int)WaterState.State.Hot;
			}
			else if (currentTemp >= 50)
			{
				return (int)WaterState.State.Warm;
			}
			else if (currentTemp >= 25)
			{
				return (int)WaterState.State.Normal;
			}
			else if (currentTemp >= 10)
			{
				return (int)WaterState.State.Cool;
			}
			else if (currentTemp >= 0)
			{
				return (int)WaterState.State.Cold;
			}
			water.Temperature = 200;
			return (int)WaterState.State.Solid;
		}

		private void RadiateTemperature()
		{
			// spread temperature to surrounding cell
			foreach (Water neighbour in water.Neighbours)
			{
				if (neighbour != null)
				{
					neighbour.Temperature += (int)(water.Temperature * (water.Temperature > neighbour.Temperature ? 1 : -1) / 20);
				}
			}
		}
	}
}
