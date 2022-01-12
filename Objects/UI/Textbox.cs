using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects;
using Framework.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Framework.Objects.UI
{
	public class Textbox : UIControl, IGameObjectComponent
	{
		public Textbox(string name, string layer, string text, SpriteFont font, Color textColor)
			: base(layer, font, textColor)
		{
			RenderMode = Rendering.RenderMode.Text;
			Name = name;
			Text = text;
		}
	}
}
