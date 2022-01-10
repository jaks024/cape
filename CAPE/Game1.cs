using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Framework.Rendering;
using Framework.Utilities;
using Framework.Objects.UI;
using Framework.Managers;
using Framework.Debug;

namespace Framework
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private ObjectPainter objPainter;

		private GameObjectManager globalGameObjectManager;
		private Renderer objectRenderer;
		
		// temp testing
		private Textbox fpsTextbox;
		private double lastRecordedGameTime = 0;

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
			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.ApplyChanges();


			base.Initialize();
		}

		protected override void LoadContent()
		{
			objectRenderer = new Renderer(GraphicsDevice);
			SpriteFont font = Content.Load<SpriteFont>("Fonts/defaultFont");

			Logger.DebugUIManager =  new DebugUIManager("DEBUG", 0, font, objectRenderer);

			globalGameObjectManager = new GameObjectManager(objectRenderer);

			objPainter = new ObjectPainter(globalGameObjectManager);

			AddTempSquares();


			objectRenderer.AddLayer("UI", 5);
			fpsTextbox = new Textbox("fpsTextbox", "UI", "hello world !", font);
			objectRenderer.AddObjectToLayer("UI", fpsTextbox);
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

			// move under mouse position
			//fpsTextbox.Position = mouseState.Position.ToVector2();

			lastRecordedGameTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (lastRecordedGameTime > 0.1f)
			{
				fpsTextbox.Text = $"fps: {(int)(1 / gameTime.ElapsedGameTime.TotalSeconds)}";
				lastRecordedGameTime = 0;
			}
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			objectRenderer.Draw();

			base.Draw(gameTime);
		}


		private void AddTempSquares()
		{
			//int squareSize = 10;
			string bgLayer = "BACKGROUND";
			string fgLayer = "FOREGROUND";
			objectRenderer.AddLayer(bgLayer, 1);
			objectRenderer.AddLayer(fgLayer, 2);

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
