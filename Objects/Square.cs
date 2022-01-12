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
			Texture2D = Utilities.TextureFactory.SolidColor(size, size);
			TextureColor = color;
			Position = position;
			RenderMode = Rendering.RenderMode.Default;
		}
	}
}
