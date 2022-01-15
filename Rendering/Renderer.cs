using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Framework.Debug;
using Framework.Interfaces;
using Framework.Objects.UI;

namespace Framework.Rendering
{
	public class Renderer
	{

		private Dictionary<string, Layer> layerDictionary;
		private SpriteBatch spriteBatch;

		public Renderer(GraphicsDevice gd)
		{
			layerDictionary = new Dictionary<string, Layer>();
			spriteBatch = new SpriteBatch(gd);
		}

		public void AddLayer(string layerName, int zIndex)
		{
			layerDictionary.TryAdd(layerName, new Layer(layerName, zIndex));
			//Logger.Print("added layer {0}", layerName);
		}

		public void RemoveLayer(string layerName)
		{
			layerDictionary.Remove(layerName);
		}

		public void AddObjectToLayer(string layerName, IRenderable obj)
		{
			if (obj == null)
			{
				return;
			}
			layerDictionary.TryGetValue(layerName, out Layer layer);
			if (layer != null)
			{
				layer.Add(obj);
				//Logger.Print("added {0} to layer {1}", obj.Name, layerName);
			} 
			else
			{
				//Logger.Print("failed to add {0} to {1}, because layer doesn't exist", obj.Name, layerName);
			}
		}

		public void RemoveObjectFromLayer(string layerName, IRenderable obj)
		{
			layerDictionary.TryGetValue(layerName, out Layer layer);
			if (layer != null)
			{
				layer.Remove(obj);
			}
		}

		public void HideLayer(string layerName)
		{
			layerDictionary.TryGetValue(layerName, out Layer layer);
			if (layer != null)
			{
				layer.SetVisibility(false);
			}
		}

		public void EnableLayer(string layerName)
		{
			layerDictionary.TryGetValue(layerName, out Layer layer);
			if (layer != null)
			{
				layer.SetVisibility(true);
			}
		}

		private void DrawString(UIControl uiControl)
		{
			spriteBatch.DrawString(uiControl.Font,
				uiControl.Text,
				uiControl.Position,
				uiControl.TextColor,
				uiControl.Rotation,
				uiControl.Origin,
				uiControl.Scale,
				uiControl.SpriteEffect,
				uiControl.LayerDepth);
		}

		private void DrawObject(IRenderable obj)
		{
			spriteBatch.Draw(obj.Texture2D,
				obj.Position,
				null,
				obj.TextureColor,
				obj.Rotation,
				obj.Origin,
				obj.Scale,
				obj.SpriteEffect,
				obj.LayerDepth);
		}

		public void Draw()
		{
			// SUPER INEFFICIENT!!!! CHANGE DATA STRUCTURE LATER
			List<Layer> layersToRenderInOrder = new List<Layer>(layerDictionary.Values);
			layersToRenderInOrder.Sort();

			//Logger.Print("drawing {0} layers", layersToRenderInOrder.Count);

			foreach(Layer layer in layersToRenderInOrder)
			{
				if (!layer.IsVisible)
				{
					//Logger.Print("skipped layer {0} due to visibility", layer.Name);
					continue;
				}

				//Logger.Print("drawing layer {0}: {1}", layer.Name, layer.ZIndex);

				foreach(IRenderable obj in layer.Drawables)
				{
					spriteBatch.Begin(SpriteSortMode.Texture);
					//Logger.Print("drawing object: {0}", obj.Name);
					switch (obj.RenderMode)
					{
						case RenderMode.Text:
							UIControl uiControl = (UIControl)obj;
							DrawString(uiControl);
							break;
						case RenderMode.Default:
							//Logger.Print("drawn object {0}", obj.Name);
							DrawObject(obj);
							break;
						default:
							continue;
					}
					spriteBatch.End();
				}

			}
		}
	}
}
