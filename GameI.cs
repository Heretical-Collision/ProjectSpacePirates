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

namespace ProjectSpaceProject
{
    public class GameI : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        public List<Controller> controllers = new List<Controller>();
        public Dictionary<String, Texture2D> spriteList = new Dictionary<String, Texture2D>();
        private Dictionary<String, SpriteFont> fonts = new Dictionary<String, SpriteFont>();
        public GameWorld gameWorld;

        private float updateMilliSeconds = 0;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (FileInfo t in new DirectoryInfo(Content.RootDirectory).GetFiles("*_img.xnb")) //При нажатии кнопки build в content.mgcb редакторе, все файлы конвертируются в .xnb файлы
            {                                                                                      //все - это картинки, шрифты, звуки, и т.д., поэтому чтобы дать понять автоматической
                                                                                                   //загрузке файлов, что данный файл именно картинка, перед расширением в названии должно стоять _img
                spriteList.Add(t.Name.Split("_img")[0], Content.Load<Texture2D>(t.Name.Split('.')[0]));
            }
            fonts.Add("FRM325", Content.Load<SpriteFont>("FRM325"));
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
            updateMilliSeconds = (float)gameTime.ElapsedGameTime.Milliseconds;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameWorld.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(fonts["FRM325"], "TPS: " + Convert.ToInt32(1000 / updateMilliSeconds), new Vector2(_graphics.PreferredBackBufferWidth - 200, _graphics.PreferredBackBufferHeight - 20), Color.Black);
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