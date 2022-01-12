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
		public Vector2 PositionOffset { get; set; }
		private Vector2 _position;
		public Vector2 Position { 
			get => _position + PositionOffset; 
			set
			{
				_position = value;
				if (Children != null)
				{
					foreach (GameObject child in Children)
					{
						child.PositionOffset = value;
					}
				}
			}
		}
		public float Rotation { get; set; }
		public float Scale { get; set; }
		public Color TextureColor { get; set; }
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
					_parent = null;
					PositionOffset = Vector2.Zero;
				}
				else
				{
					_parent?.Children.Remove(this);
					_parent = value;
					_parent.Children.Add(this);
					PositionOffset = Parent.Position;
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
			TextureColor = Color.White;
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
			child.Parent = this;
		}

		public void RemoveChild(GameObject child)
		{
			child.Parent = null;
		}

		public override string ToString()
		{
			return string.Format("Name:{0} | Position:{1} | Rotation:{2} | Scale:{3}" +
				" | Color:{4} | Origin:{5} | Enabled: {6} | Child Count:{7}",
				Name, Position, Rotation, Scale, 
				TextureColor, Origin, IsEnabled, Children.Count);
		}
	}
}
