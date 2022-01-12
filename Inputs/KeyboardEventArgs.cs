using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Framework.Inputs
{
	public class KeyboardEventArgs
	{
		public Keys Key { get; set; }
		public KeyboardEventArgs(Keys key)
		{
			Key = key;
		}
	}
}
