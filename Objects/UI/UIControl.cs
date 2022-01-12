using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Framework.Objects.UI
{
	public delegate void UIEventHandler(GameObject sender, UIEventArgs args);
	public abstract class UIControl : GameObject
	{
		public string Text { get; set; }
		public SpriteFont Font { get; set; }
		public Color TextColor { get; set; }

		public event UIEventHandler OnMouseEnter;
		public event UIEventHandler OnClick;
		public event UIEventHandler OnDrag;
		public event UIEventHandler OnRelease;

		public UIControl() { }
		public UIControl(string layer, SpriteFont font, Color textColor)
		{
			Layer = layer;
			Font = font;
			TextColor = textColor;
		}

		public UIControl(string layer, SpriteFont font, Vector2 dimension, bool isBackgroundEnabled)
		{
			Layer = layer;
			Font = font;
			RenderMode = isBackgroundEnabled ? Rendering.RenderMode.Default : Rendering.RenderMode.Skip;
			Texture2D = Utilities.TextureFactory.SolidColor(dimension.X, dimension.Y);
		}

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
