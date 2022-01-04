using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace wireman
{
	public static class Renderer
	{
		public static List<string> ExistingLayers { get; private set; } = new List<string>();

		private static Dictionary<string, Layer> layerDictionary = new Dictionary<string, Layer>();
		private static SpriteBatch spriteBatch;

		public static void Initialize(GraphicsDevice gd)
		{
			spriteBatch = new SpriteBatch(gd);
		}

		public static void AddLayer(string layerName, int zIndex)
		{
			layerDictionary.Add(layerName, new Layer(layerName, zIndex));
			ExistingLayers.Add(layerName);
			Logger.Print("added layer {0}", layerName);
		}

		public static void RemoveLayer(string layerName)
		{
			layerDictionary.Remove(layerName);
			ExistingLayers.Remove(layerName);
		}

		public static void AddObjectToLayer(string layerName, IDrawable obj)
		{
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

		public static void RemoveObjectFromLayer(string layerName, IDrawable obj)
		{
			layerDictionary.TryGetValue(layerName, out Layer layer);
			if (layer != null)
			{
				layer.Remove(obj);
			}
		}

		public static void HideLayer(string layerName)
		{
			layerDictionary.TryGetValue(layerName, out Layer layer);
			if (layer != null)
			{
				layer.SetVisibility(false);
			}
		}

		public static void EnableLayer(string layerName)
		{
			layerDictionary.TryGetValue(layerName, out Layer layer);
			if (layer != null)
			{
				layer.SetVisibility(true);
			}
		}

		public static void Draw()
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
				foreach(IDrawable obj in layer.Drawables)
				{
					//Logger.Print("drawn object: {0}", obj.Name);
					spriteBatch.Draw(obj.Texture2D, obj.Position, null, obj.Color, obj.Rotation,
						obj.Origin, obj.Scale, obj.SpriteEffect, obj.LayerDepth); 
				}
				spriteBatch.End();
			}
		}
	}
}
