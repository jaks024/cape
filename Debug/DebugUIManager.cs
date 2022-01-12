using Framework.Managers;
using Framework.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Debug
{
	public class DebugUIManager
	{
		private OnScreenConsole uiConsole;
		private ObjectValueDisplayer valueDisplayer;
		private string debugRenderLayer;
		private Renderer uiRenderer;
		public DebugUIManager(string debugLayerName, int debugLayerIndex,  Renderer renderer)
		{
			debugRenderLayer = debugLayerName;
			uiRenderer = renderer;

			uiRenderer.AddLayer(debugLayerName, debugLayerIndex);
		}

		public void InitializeConsole(SpriteFont font, Color consoleColor, 
			Color textColor, Vector2 consoleDimension, bool enableBackground)
		{
			uiConsole = new OnScreenConsole(debugRenderLayer, font, consoleDimension, enableBackground)
			{
				TextureColor = consoleColor,
				TextColor = textColor
			};

			uiRenderer.AddObjectToLayer(debugRenderLayer, uiConsole);
		}

		public void InitializeValueDisplayer(SpriteFont font, Color displayerColor, Color textColor, 
			Vector2 displayerDimension, bool enableBackground, GameObjectManager gameObjectManager)
		{
			valueDisplayer = new ObjectValueDisplayer(debugRenderLayer, font,
				displayerDimension, enableBackground, gameObjectManager)
			{
				TextureColor = displayerColor,
				TextColor = textColor,
				Position = new Vector2(200, 25)
			};

			uiRenderer.AddObjectToLayer(debugRenderLayer, valueDisplayer);
		}

		public void MsgConsole(string message, params object[] args)
		{
			uiRenderer.AddObjectToLayer(debugRenderLayer, 
				uiConsole.NewMessage(string.Format(message, args)));
		}

		public void SetConsolePosition(Vector2 position)
		{
			uiConsole.Position = position;
		}

		public void AddObjectToDisplayer(ulong id)
		{
			uiRenderer.AddObjectToLayer(debugRenderLayer, valueDisplayer.AddObject(id));
		}
		public void RemoveObjectFromDisplayer(ulong id)
		{
			uiRenderer.RemoveObjectFromLayer(debugRenderLayer, valueDisplayer.Remove(id));
		}
		public void RefreshDisplayer()
		{
			valueDisplayer.Refresh();
		}

		public void SetValueDisplayerPosition(Vector2 position)
		{
			valueDisplayer.Position = position;
		}
	}
}
