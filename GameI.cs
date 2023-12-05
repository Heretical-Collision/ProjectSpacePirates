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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<Controller> controllers = new List<Controller>();
        public List<Texture2D> spriteList = new List<Texture2D>();
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
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteList.Add(Content.Load<Texture2D>("sprite_ship"));
            spriteList.Add(Content.Load<Texture2D>("defaultch"));
            gameWorld = CreateWorld();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
           //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
               //Exit();
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
    }
}