using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Framework.Interfaces;
using Framework.Debug;

namespace Framework.Objects
{
	public class GameObject : IRenderable
	{
		public ulong Id { get; set; }
		public string Name { get; set; }
		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		public float Scale { get; set; }
		public Color Color { get; set; }
		public Vector2 Origin { get; protected set; }
		public SpriteEffects SpriteEffect { get; set; }
		public float LayerDepth { get; set; }
		public Texture2D Texture2D { get; set; }
		public bool IsEnabled { get; set; }
		public string Layer { get; set; }
		public Rendering.RenderMode RenderMode { get; protected set; }
		public List<IGameObjectComponent> AttachedComponents { get; protected set; }
		
		public List<GameObject> Children { get; protected set; }
		private GameObject _parent;
		public GameObject Parent { 
			get => _parent;
			set
			{
				if (value == null)
				{
					_parent?.Children.Remove(this);
					_parent = value;

					Origin = Vector2.Zero;
				}
				else
				{
					_parent?.Children.Remove(this);
					_parent = value;
					_parent.Children.Add(this);

					Origin = _parent.Position;
				}
			}
		}


		public GameObject()
		{
			Initialize();
		}

		public GameObject(string name, string layer)
		{
			Initialize();
			Name = name;
			Layer = layer;
		}

		private void Initialize()
		{
			Name = string.Empty;
			Position = Vector2.Zero;
			Rotation = 0;
			Scale = 1;
			Color = Color.White;
			Origin = Vector2.Zero;
			SpriteEffect = SpriteEffects.None;
			LayerDepth = 0;
			Texture2D = null;
			IsEnabled = true;
			Layer = string.Empty;
			RenderMode = Rendering.RenderMode.Default;
			AttachedComponents = new List<IGameObjectComponent>();
			Children = new List<GameObject>();
			Parent = null;
			RenderMode = Rendering.RenderMode.Skip;
		}

		public void AddChild(GameObject child)
		{
			Children.Add(child);
			child.Parent = this;
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
