using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Framework.Inputs
{
	public class InputEventManager
	{
		public delegate void KeyboardEventHanlder(object sender, KeyboardEventArgs args);
		public delegate void MouseEventHandler(object sender, MouseEventArgs args);
		public event KeyboardEventHanlder KeyboardEvent;
		public event MouseEventHandler MouseEvent;
		
		private Dictionary<Keys, Keys> keyDictionary;
		private Vector2 lastMousePosition;
		public InputEventManager() 
		{
			keyDictionary = new Dictionary<Keys, Keys>();
			lastMousePosition = Mouse.GetState().Position.ToVector2();
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
					KeyboardEvent?.Invoke(this, new KeyboardEventArgs(keyDictionary[key]));
				}
			}

			if (MouseEvent != null)
			{
				MouseState state = Mouse.GetState();
				Vector2 mousePos = state.Position.ToVector2();
				if (state.LeftButton.HasFlag(ButtonState.Pressed))
					InvokeMouseEvent(MouseKeyStates.LeftPressed, mousePos);

				if (state.RightButton.HasFlag(ButtonState.Pressed))
					InvokeMouseEvent(MouseKeyStates.RightPressed, mousePos);

				if (state.MiddleButton.HasFlag(ButtonState.Pressed))
					InvokeMouseEvent(MouseKeyStates.MiddlePressed, mousePos);

				if (!lastMousePosition.Equals(mousePos))
				{
					InvokeMouseEvent(MouseKeyStates.Nothing, mousePos);
					lastMousePosition = mousePos;
				}
			}
		}

		private void InvokeMouseEvent(MouseKeyStates state, Vector2 position)
		{
			MouseEvent?.Invoke(this, new MouseEventArgs(state, position));
		}

	}
}
