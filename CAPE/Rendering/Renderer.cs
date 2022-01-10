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
			layerDictionary.Add(layerName, new Layer(layerName, zIndex));
			Logger.Print("added layer {0}", layerName);
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
				Logger.Print("added {0} to layer {1}", obj.Name, layerName);
			} 
			else
			{
				Logger.Print("failed to add {0} to {1}, because layer doesn't exist", obj.Name, layerName);
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
				spriteBatch.Begin(SpriteSortMode.BackToFront);
				foreach(IRenderable obj in layer.Drawables)
				{
					//Logger.Print("drawing object: {0}", obj.Name);
					switch (obj.RenderMode)
					{
						case RenderMode.Text:
							UIControl uiControl = (UIControl)obj;
							spriteBatch.DrawString(uiControl.Font, 
								uiControl.Text, 
								obj.Position,
								obj.Color,
								obj.Rotation,
								obj.Origin,
								obj.Scale,
								obj.SpriteEffect,
								obj.LayerDepth);
							break;
						case RenderMode.Default:
							spriteBatch.Draw(obj.Texture2D,
								obj.Position,
								null,
								obj.Color,
								obj.Rotation,
								obj.Origin,
								obj.Scale,
								obj.SpriteEffect,
								obj.LayerDepth);
							break;
						default:
							continue;
					}

					


				}
				spriteBatch.End();
			}
		}
	}
}
