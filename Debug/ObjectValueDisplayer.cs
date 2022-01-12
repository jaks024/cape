using Framework.Managers;
using Framework.Objects;
using Framework.Objects.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Debug
{
	public class ObjectValueDisplayer : UIControl
	{
		private Dictionary<ulong, Textbox> objectsToShow;
		private GameObjectManager objectManager;
		private int currentLine = -1;
		private int lineSpacing = 15;
		public ObjectValueDisplayer(string layer, SpriteFont font, Vector2 dimension, bool isBackgroundEnabled, GameObjectManager objManager)
			: base(layer, font, dimension, isBackgroundEnabled)
		{
			objectsToShow = new Dictionary<ulong, Textbox>();
			objectManager = objManager;
		}

		public Textbox AddObject(ulong id)
		{
			++currentLine;

			Textbox tb = new Textbox(string.Empty, Layer, objectManager.Get(id).ToString(), Font, TextColor);
			tb.Position = new Vector2(tb.Position.X, currentLine * lineSpacing);
			objectsToShow.Add(id, tb);
			tb.Scale = 0.8f;
			AddChild(tb);

			return tb;
		}

		public Textbox Remove(ulong id)
		{
			--currentLine;

			Textbox temp = objectsToShow[id];
			objectsToShow.Remove(id);
			RemoveChild(temp);
			return temp;
		}

		public void Refresh()
		{
			foreach(ulong key in objectsToShow.Keys)
			{
				objectsToShow[key].Text = objectManager.Get(key).ToString(); 
			}
		}

	}
}
