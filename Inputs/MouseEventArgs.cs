using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Inputs
{
	public enum MouseKeyStates
	{
		Nothing = -1,
		LeftPressed = 0,
		LeftReleased = 1,
		RightPressed = 2,
		RightReleased = 3,
		MiddlePressed = 4,
		MiddleReleased = 5
	}

	public class MouseEventArgs
	{
		public Vector2 Position { get; set; }
		public MouseKeyStates MouseKeyState { get; set; }
		public MouseEventArgs(MouseKeyStates mouseKeyState, Vector2 position)
		{
			MouseKeyState = mouseKeyState;
			Position = position;
		}

	}
}
