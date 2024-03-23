using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ProjectSpaceProject.ECS;
using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.ECS.Sprite;
using ProjectSpaceProject.ECS.Control;
using ProjectSpaceProject.ECS.Collision;
using System.ComponentModel;
using System.Xml.Linq;
using ProjectSpaceProject.EventManage;
using static ProjectSpaceProject.StaticECSMethods;

namespace ProjectSpaceProject
{
    public class GameWorld
    {
        public GameI gameInstance;
        public SpriteBatch spriteBatch;                                             
        public float screenScale = 1;
        public Matrix matrix;
        public PlayerController ClientPlayerController { get { return (gameInstance.controllers.Count > 0) ? (gameInstance.controllers[0] as PlayerController) : null; } }
        public MapGenerator mapGenerator;
        public List<Entity> entities = new List<Entity>();
        public Dictionary<Type, (List<BaseComponent>, int)> componentsByType = new Dictionary<Type, (List<BaseComponent>, int)>();
        public BaseSystem[] systems;
        public SpriteSystem spriteSystem;
        public ServerSystemManager serverManager;
        public EventManager eventManager;


        public void Update(GameTime gameTime)
        {
            serverManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (gameInstance.controllers.Count > 0)
            {

                spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack,
                    samplerState: SamplerState.PointClamp,
                    transformMatrix: Matrix.CreateTranslation(-ClientPlayerController.cameraLocation.X, -ClientPlayerController.cameraLocation.Y, 0f) *
                        Matrix.CreateScale(ClientPlayerController.cameraZoom * screenScale, ClientPlayerController.cameraZoom * screenScale, 1f) *
                        Matrix.CreateTranslation(gameInstance._graphics.PreferredBackBufferWidth / 2, gameInstance._graphics.PreferredBackBufferHeight / 2, 0),
                    blendState: BlendState.NonPremultiplied);

                spriteSystem.Draw();

                spriteBatch.End();
            }
        }

        public GameWorld(GameI _gameInstance, SpriteBatch _spriteBatch) 
        {
            gameInstance = _gameInstance;
            spriteBatch = _spriteBatch;
            StaticECSMethods.gameWorld = this;
            Run();
        }

        public void Run()
        {
            gameInstance.controllers.Add(new PlayerController(gameInstance, this));

            serverManager = new ServerSystemManager(this);

            AddPlayer(new Vector2(0, 0));

            mapGenerator = new MapGenerator(this);
        }

        public void AdaptiveScreenScale()
        {
            screenScale = gameInstance._graphics.PreferredBackBufferWidth / 1280 * 1;
        }
    }
}
    