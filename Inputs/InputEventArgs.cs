using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;


namespace Framework.Inputs
{
	public class InputEventArgs
	{
		public Keys Key { get; set; }
		public MouseState MouseState { get; set; }

		public InputEventArgs(Keys key)
		{
			Key = key;
		}
		public InputEventArgs(MouseState mouseState)
		{
			MouseState = mouseState;
		}
	}
}
