using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Framework.Rendering;
using Framework.Utilities;
using Framework.Objects.UI;
using Framework.Managers;
using Framework.Debug;
using Framework.Inputs;

namespace Framework
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private ObjectPainter objPainter;

		private GameObjectManager globalGameObjectManager;
		private Renderer objectRenderer;
		private InputEventManager inputManager;
		
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
			TextureFactory.GraphicsDevice = GraphicsDevice;

			objectRenderer = new Renderer(GraphicsDevice);
			SpriteFont font = Content.Load<SpriteFont>("Fonts/defaultFont");

			Logger.DebugUIManager =  new DebugUIManager("DEBUG", 5, objectRenderer);
			Logger.DebugUIManager.InitializeConsole(font, Color.DarkBlue, Color.White, new Vector2(300, 800), true);
			Logger.DebugUIManager.SetConsolePosition(new Vector2(0, 25));

			globalGameObjectManager = new GameObjectManager(objectRenderer);

			Logger.DebugUIManager.InitializeValueDisplayer(font, Color.DarkCyan, Color.White, new Vector2(700, 600), true, globalGameObjectManager);
			Logger.DebugUIManager.SetValueDisplayerPosition(new Vector2(300, 25));

			AddTempSquares();
			AddFPSCounter(font);

			inputManager = new InputEventManager();
			inputManager.AddKeyToListen(Keys.A, Keys.A);
			inputManager.AddKeyToListen(Keys.D, Keys.D);
			inputManager.AddKeyToListen(Keys.W, Keys.S);
			inputManager.KeyboardEvent += InputManager_KeyboardEvent;
		}

		private void InputManager_KeyboardEvent(object sender, InputEventArgs args)
		{
			Logger.PrintToUI("Key pressed: {0}", args.Key);
		}

		protected override void Update(GameTime gameTime)
		{
			inputManager.Listen();

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			if (inputManager.GetMouseLeftState(ButtonState.Pressed)) 
			{
				//Logger.Print("x: {0}, y: {1}", mouseState.X, mouseState.Y);
				objPainter.Paint(inputManager.GetMousePosition(), GraphicsDevice, "FOREGROUND");
			}

			if (inputManager.GetMouseRightState(ButtonState.Pressed))
			{
				objPainter.Erase(inputManager.GetMousePosition(), "FOREGROUND");
			}

			if (inputManager.GetMouseMiddleState(ButtonState.Pressed))
			{
				objPainter.Clear();
			}

			Logger.DebugUIManager.RefreshDisplayer();
			//Logger.DebugUIManager.SetValueDisplayerPosition(mouseState.Position.ToVector2());

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

		private void AddFPSCounter(SpriteFont font)
		{
			objectRenderer.AddLayer("UI", 0);
			fpsTextbox = new Textbox("fpsTextbox", "UI", "hello world !", font, Color.Lime);
			objectRenderer.AddObjectToLayer("UI", fpsTextbox);
		}

		private void AddTempSquares()
		{
			objPainter = new ObjectPainter(globalGameObjectManager);
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
