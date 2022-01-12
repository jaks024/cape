using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects;


namespace Framework.Interfaces
{
	public interface IRenderable
	{
		public string Name { get; }
		public Vector2 Position { get; }
		public float Rotation { get; }
		public float Scale { get; }
		public Color TextureColor { get; }
		public Vector2 Origin { get; }
		public SpriteEffects SpriteEffect { get; }
		public float LayerDepth { get; }
		public Texture2D Texture2D { get; }
		public bool IsEnabled { get; }
		public Rendering.RenderMode RenderMode { get; }
		public List<IGameObjectComponent> AttachedComponents { get; }
	}
}
