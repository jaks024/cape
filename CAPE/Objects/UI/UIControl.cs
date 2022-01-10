using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects;
using Framework.Objects.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Framework.Objects.UI
{
	public delegate void UIEventHandler(GameObject sender, UIEventArgs args);
	public abstract class UIControl : GameObject
	{
		public SpriteFont Font { get; set; }
		public string Text { get; set; }

		public event UIEventHandler OnMouseEnter;
		public event UIEventHandler OnClick;
		public event UIEventHandler OnDrag;
		public event UIEventHandler OnRelease;

		public void RaiseOnMouseEnterEvent(GameObject sender, UIEventArgs args)
		{
			OnMouseEnter?.Invoke(sender, args);
		}
		public void RaiseOnClickEvent(GameObject sender, UIEventArgs args)
		{
			OnClick?.Invoke(sender, args);
		}
		public void RaiseOnDragEvent(GameObject sender, UIEventArgs args)
		{
			OnDrag?.Invoke(sender, args);
		}
		public void RaiseOnReleaseEvent(GameObject sender, UIEventArgs args)
		{
			OnRelease?.Invoke(sender, args);
		}

	}
}
