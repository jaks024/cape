using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utilities
{
	public static class TextureFactory
	{
		public static GraphicsDevice GraphicsDevice { get; set; }

		public static Texture2D SolidColor(float width, float height)
		{
			int iWidth = (int)width;
			int iHeight = (int)height;
			Texture2D texture = new Texture2D(GraphicsDevice, iWidth, iHeight);

			Color[] data = new Color[iWidth * iHeight];
			for (int i = 0; i < data.Length; ++i)
			{
				data[i] = Color.White;
			}
			texture.SetData(data);
			return texture;
		}

	}
}
