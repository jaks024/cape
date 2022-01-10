using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects;
using Framework.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Framework.Objects.UI
{
	public class Textbox : UIControl, IGameObjectComponent
	{
		public Textbox(string name, string layer, string text, SpriteFont font)
		{
			RenderMode = Rendering.RenderMode.Text;
			Name = name;
			Layer = layer;
			Font = font;
			Text = text;
		}
	}
}
