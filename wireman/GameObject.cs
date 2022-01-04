using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace wireman
{
	public class GameObject : IDrawable
	{
		public string Name { get; protected set; }
		public Vector2 Position { get; protected set; }
		public float Rotation { get; protected set; }
		public float Scale { get; protected set; }
		public Color Color { get; protected set; }
		public Vector2 Origin { get; protected set; }
		public SpriteEffects SpriteEffect { get; protected set; }
		public float LayerDepth { get; protected set; }
		public Texture2D Texture2D { get; protected set; }
		public bool IsEnabled { get; protected set; }
		public string Layer { get; protected set; }
		
		// add list of components later (colliders)

		public GameObject(string name, string layer)
		{
			Name = name;
			Position = Vector2.Zero;
			Rotation = 0;
			Scale = 1;
			Color = Color.White;
			Origin = Vector2.Zero;
			SpriteEffect = SpriteEffects.None;
			LayerDepth = 0;
			Texture2D = null;
			IsEnabled = true;
			Layer = layer;
		}

		public override string ToString()
		{
			return string.Format("Name:{0}\nPosition:{1}\nRotation:{2}\nScale:{3}" +
				"\nColor:{4}\nSpriteEffect:{5}\nLayerDepth:{6}\nTexture2D:{7}",
				Name, Position, Rotation, Scale, 
				Color, SpriteEffect, LayerDepth, Texture2D == null ? "Null" : "Exist");
		}
	}
}
