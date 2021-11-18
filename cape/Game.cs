using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace cape
{
	public class Game : GameWindow
	{
		public Game(GameWindowSettings gameSettings, NativeWindowSettings  nativeSettings) : base(gameSettings, nativeSettings) { }

		protected override void OnLoad()
		{
			GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

			base.OnLoad();
		}

		protected override void OnRenderFrame(FrameEventArgs args)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);   // clears the screen


			Context.SwapBuffers();
			base.OnRenderFrame(args);
		}

		protected override void OnResize(ResizeEventArgs e)
		{
			GL.Viewport(0, 0, Size.X, Size.Y);

			base.OnResize(e);
		}


		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			if (KeyboardState.IsKeyDown(Keys.Escape))
			{
				Close();
			}

			base.OnUpdateFrame(args);
		}
	}
}
