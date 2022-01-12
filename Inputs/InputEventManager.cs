using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Framework.Inputs
{
	public class InputEventManager
	{
		public delegate void InputEventHanlder(object sender, InputEventArgs args);
		public event InputEventHanlder KeyboardEvent;

		private Dictionary<Keys, Keys> keyDictionary;

		public InputEventManager() 
		{
			keyDictionary = new Dictionary<Keys, Keys>();
		}

		public bool AddKeyToListen(Keys bindedKey, Keys actualKey)
		{
			return keyDictionary.TryAdd(bindedKey, actualKey);
		}

		public bool RemoveKeyToListen(Keys bindedKey)
		{
			return keyDictionary.Remove(bindedKey);
		}

		public void Listen()
		{
			foreach (Keys key in keyDictionary.Keys)
			{
				if (Keyboard.GetState().IsKeyDown(key))
				{
					KeyboardEvent?.Invoke(this, new InputEventArgs(keyDictionary[key]));
				}
			}
		}

		public bool GetMouseLeftState(ButtonState state)
		{
			return Mouse.GetState().LeftButton.HasFlag(state);
		}

		public bool GetMouseRightState(ButtonState state)
		{
			return Mouse.GetState().RightButton.HasFlag(state);
		}

		public bool GetMouseMiddleState(ButtonState state)
		{
			return Mouse.GetState().MiddleButton.HasFlag(state);
		}

		public Vector2 GetMousePosition()
		{
			return Mouse.GetState().Position.ToVector2();
		}

	}
}
