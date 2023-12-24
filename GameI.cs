using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject
{
    public class GameI : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<Controller> controllers = new List<Controller>();
        public Dictionary<String, Texture2D> spriteList = new Dictionary<String, Texture2D>();
        public GameWorld gameWorld;
        public GameI()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.HardwareModeSwitch = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteList.Add("defaultch_right", Content.Load<Texture2D>("defaultch_right"));
            spriteList.Add("defaultch_down", Content.Load<Texture2D>("defaultch_down"));
            spriteList.Add("defaultch_left", Content.Load<Texture2D>("defaultch_left"));
            spriteList.Add("defaultch_up", Content.Load<Texture2D>("defaultch_up"));
            gameWorld = CreateWorld();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (Controller controller in controllers) 
            {
                controller.Update(gameTime);
            }

            gameWorld.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameWorld.Draw(gameTime);
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected GameWorld CreateWorld()
        {
            return new GameWorld(this, _spriteBatch);
        }

        public void SwitchFullScreenMode()
        {
            if (_graphics.IsFullScreen)
            {
                _graphics.PreferredBackBufferWidth = 1280;
                _graphics.PreferredBackBufferHeight = 720;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            }
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            _graphics.ApplyChanges();
            gameWorld.AdaptiveScreenScale();
        }


    }
}