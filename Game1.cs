using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Framework.Rendering;
using Framework.Utilities;
using Framework.Objects.UI;
using Framework.Managers;
using Framework.Debug;
using Framework.Inputs;
using Framework.Objects.CellularAutomata;

namespace Framework
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private ObjectPainter objPainter;

		private CellMapGenerator cellMapGenerator;


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

			Logger.DebugUIManager =  new DebugUIManager("DEBUG", 0, objectRenderer);
			Logger.DebugUIManager.InitializeConsole(font, Color.DarkBlue, Color.White, new Vector2(300, 800), true);
			Logger.DebugUIManager.SetConsolePosition(new Vector2(0, 25));

			globalGameObjectManager = new GameObjectManager(objectRenderer);

			Logger.DebugUIManager.InitializeValueDisplayer(font, Color.DarkCyan, Color.White, new Vector2(700, 600), true, globalGameObjectManager);
			Logger.DebugUIManager.SetValueDisplayerPosition(new Vector2(300, 25));


			string mapLayer = "MAP";
			objectRenderer.AddLayer(mapLayer, 10);
			cellMapGenerator = new CellMapGenerator(0, mapLayer, globalGameObjectManager);
			


			AddTempSquares();
			AddFPSCounter(font);

			inputManager = new InputEventManager();
			inputManager.AddKeyToListen(Keys.A, Keys.A);
			inputManager.AddKeyToListen(Keys.D, Keys.D);
			inputManager.AddKeyToListen(Keys.W, Keys.S);
			inputManager.AddKeyToListen(Keys.Escape, Keys.Escape);
			inputManager.KeyboardEvent += InputManager_KeyboardEvent;
			inputManager.MouseEvent += InputManager_MouseEvent;
		}

		private void InputManager_MouseEvent(object sender, MouseEventArgs args)
		{
			switch (args.MouseKeyState)
			{
				case MouseKeyStates.LeftPressed:
					objPainter.Paint(args.Position, GraphicsDevice, "FOREGROUND");
					break;
				case MouseKeyStates.RightPressed:
					objPainter.Erase(args.Position, "FOREGROUND");
					break;
				case MouseKeyStates.MiddlePressed:
					objPainter.Clear();
					break;
			}
		}

		private void InputManager_KeyboardEvent(object sender, KeyboardEventArgs args)
		{
			Logger.PrintToUI("Key pressed: {0}", args.Key);

			if (args.Key == Keys.Escape)
			{
				Exit();
			}
		}

		protected override void Update(GameTime gameTime)
		{
			inputManager.Listen();
			cellMapGenerator.Update();

			Logger.DebugUIManager.RefreshDisplayer();

			UpdateFPS(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			objectRenderer.Draw();

			base.Draw(gameTime);
		}

		private void UpdateFPS(GameTime gameTime)
		{
			lastRecordedGameTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (lastRecordedGameTime > 0.1f)
			{
				fpsTextbox.Text = $"fps: {(int)(1 / gameTime.ElapsedGameTime.TotalSeconds)}";
				lastRecordedGameTime = 0;
			}
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
			objectRenderer.AddLayer(fgLayer, 15);

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
