using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wireman
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private ObjectPainter objPainter;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			graphics.SynchronizeWithVerticalRetrace = false;
			IsFixedTimeStep = false;
		}

		protected override void Initialize()
		{
			Renderer.Initialize(GraphicsDevice);

			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.ApplyChanges();


			base.Initialize();
		}

		protected override void LoadContent()
		{
			objPainter = new ObjectPainter();

			AddTempSquares();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			MouseState mouseState = Mouse.GetState();
			if (mouseState.LeftButton.HasFlag(ButtonState.Pressed)) 
			{
				//Logger.Print("x: {0}, y: {1}", mouseState.X, mouseState.Y);
				objPainter.Paint(mouseState.Position.ToVector2(), GraphicsDevice, "FOREGROUND");
			}

			if (mouseState.RightButton.HasFlag(ButtonState.Pressed))
			{
				objPainter.Erase(mouseState.Position.ToVector2(), "FOREGROUND");
			}

			if (mouseState.MiddleButton.HasFlag(ButtonState.Pressed))
			{
				objPainter.Clear();
			}


			Logger.Print("fps: {0}", 1 / gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			Renderer.Draw();

			base.Draw(gameTime);
		}


		private void AddTempSquares()
		{
			//int squareSize = 10;
			string bgLayer = "BACKGROUND";
			string fgLayer = "FOREGROUND";
			Renderer.AddLayer(bgLayer, 0);
			Renderer.AddLayer(fgLayer, 1);

			//for (int x = 0; x < 15; x++)
			//{
			//	for (int y = 0; y < 15; y++)
			//	{
			//		Color c = new Color(new Vector3(x * 0.1f, y * 0.1f, 0.5f));
			//		Square square = new Square("square " + x + " - " + y, fgLayer, GraphicsDevice, squareSize, c, new Vector2(squareSize * x + squareSize, squareSize * y + squareSize));
			//		GlobalObjectManager.Add(square);
			//	}
			//}
			//for (int x = 0; x < 20; x++)
			//{
			//	for (int y = 0; y < 20; y++)
			//	{
			//		Color c = new Color(new Vector3(x * 0.05f, y * 0.05f, 0.1f));
			//		Square square = new Square("square " + x + " - " + y, bgLayer, GraphicsDevice, squareSize, c, new Vector2(squareSize * x + squareSize, squareSize * y + squareSize));
			//		GlobalObjectManager.Add(square);
			//	}
			//}
		}
	}
}
