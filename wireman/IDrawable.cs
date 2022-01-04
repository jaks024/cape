using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace wireman
{
	public interface IDrawable
	{
		public string Name { get; }
		public Vector2 Position { get; }
		public float Rotation { get; }
		public float Scale { get; }
		public Color Color { get; }
		public Vector2 Origin { get; }
		public SpriteEffects SpriteEffect { get; }
		public float LayerDepth { get; }
		public Texture2D Texture2D { get; }
		public bool IsEnabled { get; }
	}
}
