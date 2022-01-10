using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Framework.Objects
{
	public class Square : GameObject
	{
		public Square(string name, string layer, GraphicsDevice graphicsDevice, int size, Color color, Vector2 position) : base (name, layer)
		{
			Console.WriteLine("made square named: " + name);
			Texture2D = new Texture2D(graphicsDevice, size, size);

			Color[] data = new Color[size * size];
			for (int i = 0; i < data.Length; ++i)
			{
				data[i] = Color.White;
			}
			Texture2D.SetData(data);

			Color = color;
			Position = position;
			RenderMode = Rendering.RenderMode.Default;
		}
	}
}
