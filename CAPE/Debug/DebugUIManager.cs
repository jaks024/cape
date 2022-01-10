using Framework.Rendering;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Debug
{
	public class DebugUIManager
	{
		private OnScreenConsole uiConsole;
		private string debugRenderLayer;
		private Renderer uiRenderer;
		public DebugUIManager(string debugLayerName, int debugLayerIndex, SpriteFont debugFont, Renderer renderer)
		{
			debugRenderLayer = debugLayerName;
			uiConsole = new OnScreenConsole(debugLayerName, debugFont);

			uiRenderer = renderer;
			uiRenderer.AddLayer(debugLayerName, debugLayerIndex);
			uiRenderer.AddObjectToLayer(debugLayerName, uiConsole);
		}

		public void MsgConsole(string message, params object[] args)
		{
			uiRenderer.AddObjectToLayer(debugRenderLayer, 
				uiConsole.NewMessage(string.Format(message, args)));
		}

	}
}
