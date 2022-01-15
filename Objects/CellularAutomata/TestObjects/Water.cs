using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Objects.CellularAutomata.TestObjects
{
	public class Water : Cell
	{

		public float Temperature { get; set; }
		public float MovementSpeed { get; set; }  

		public Water(GameObject visualObject, WaterState defaultState)
			: base(visualObject, defaultState)
		{
			Temperature = 200;
			MovementSpeed = 0;
		}

		public override void UpdateVisual()
		{
			switch (currentState.CurrentState)
			{
				case (int)WaterState.State.Vapor:
					SetVisualTextureColor(245, 253, 255);
					break;
				case (int)WaterState.State.Hot:
					SetVisualTextureColor(242, 76, 34);
					break;
				case (int)WaterState.State.Warm:
					SetVisualTextureColor(237, 194, 142);
					break;
				case (int)WaterState.State.Normal:
					SetVisualTextureColor(184, 230, 223);
					break;
				case (int)WaterState.State.Cool:
					SetVisualTextureColor(138, 199, 230);
					break;
				case (int)WaterState.State.Cold:
					SetVisualTextureColor(98, 208, 245);
					break;
				case (int)WaterState.State.Solid:
					SetVisualTextureColor(105, 242, 255);
					break;
			}
		}

		private void SetVisualTextureColor(int r, int g, int b)
		{
			VisualObject.TextureColor = new Microsoft.Xna.Framework.Color(r, g, b);
		}
	}
}
