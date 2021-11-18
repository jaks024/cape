using System;
using OpenTK;
using OpenTK.Windowing.Desktop;

namespace cape
{
	class Program
	{
		static void Main(string[] args)
		{

			GameWindowSettings gameSetting = new GameWindowSettings()
			{
				IsMultiThreaded = true,
				RenderFrequency = 60,
				UpdateFrequency = 60
			};
			NativeWindowSettings nativeSetting = new NativeWindowSettings()
			{
				Title = "CAPE",
				Size = new OpenTK.Mathematics.Vector2i(1280, 720)
			};

			using (Game game = new(gameSetting, nativeSetting))
			{
				game.Run();
			}
		}
	}
}
