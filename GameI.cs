using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace ProjectSpaceProject
{
    public class GameI : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        public List<Controller> controllers = new List<Controller>();
        public Dictionary<String, Texture2D> spriteList = new Dictionary<String, Texture2D>();
        private Dictionary<String, SpriteFont> fonts = new Dictionary<String, SpriteFont>();
        private Dictionary<String, SoundEffect> sounds = new Dictionary<String, SoundEffect>();
        public GameWorld gameWorld;

        public GameI()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.HardwareModeSwitch = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.Red });
            spriteList.Add("DebugPixel", t);

            foreach (string file in Directory.EnumerateFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent + "/Resources/Sprites/", "*.png", SearchOption.AllDirectories)) //Автоматическая загрузка .png
            {
                using (var fileStream = new FileStream(file, FileMode.Open))
                {
                    Texture2D t_texture = Texture2D.FromStream(GraphicsDevice, fileStream);
                    FileInfo t_file = new FileInfo(file);
                    spriteList.Add(t_file.Name.Split(".")[0], t_texture);
                }
            }
            fonts.Add("FRM325", Content.Load<SpriteFont>("FRM325x8"));
                gameWorld = CreateWorld();
        }

        protected override void Update(GameTime gameTime)
        {
            gameWorld.Update(gameTime);
            base.Update(gameTime);
            foreach (Controller controller in controllers)
            {
                controller.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameWorld.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(fonts["FRM325"], "FPS: " + Convert.ToInt32(1000 / (float)gameTime.ElapsedGameTime.Milliseconds), new Vector2(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferHeight - 20), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected GameWorld CreateWorld()
        {
            return new GameWorld(this, spriteBatch);
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