using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpaceProject.ECS;
using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.ECS.Sprite;
using ProjectSpaceProject.ECS.Control;

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
        public List<BaseSystem> systems = new List<BaseSystem>();

        public void AddPlayer(Vector2 location)
        {
            ControlComponent tempRefControlComponent = new ControlComponent();
            entities.Add(new Entity(new List<BaseComponent>() {
                                        new MovementComponent(new Vector2(50, 50), new Vector2(0, 0), 1.5f), 
                                        new SpriteComponent(new Dictionary<string, SpriteData>(){
                                                           { "WalkRight", new SpriteData(gameInstance.spriteList["playerCharacterRight"], 1, 6, 6, 0)},
                                                           { "WalkDown", new SpriteData(gameInstance.spriteList["playerCharacterDown"], 1, 6, 6, 0)},
                                                           { "WalkLeft", new SpriteData(gameInstance.spriteList["playerCharacterLeft"], 1, 6, 6, 0)},
                                                           { "WalkUp", new SpriteData(gameInstance.spriteList["playerCharacterUp"], 1, 6, 6, 0)}},
                                            new Vector2(0, 0),
                                            1f),
                                        tempRefControlComponent}));
            ControlSystem.ConnectControllerToComponent(tempRefControlComponent, gameInstance.controllers[0]);
        }

        public void Update(GameTime gameTime)
        {
            foreach(BaseSystem sys in systems)
            {
                sys.Update();
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack,
                samplerState: SamplerState.PointClamp,
                transformMatrix: Matrix.CreateTranslation(-ClientPlayerController.cameraLocation.X, -ClientPlayerController.cameraLocation.Y, 0f) *
                    Matrix.CreateScale(ClientPlayerController.cameraZoom * screenScale, ClientPlayerController.cameraZoom * screenScale, 1f) *
                    Matrix.CreateTranslation(gameInstance._graphics.PreferredBackBufferWidth / 2, gameInstance._graphics.PreferredBackBufferHeight / 2, 0));
            (systems[1] as SpriteSystem).Draw();
            spriteBatch.End();
        }


        public GameWorld(GameI _gameInstance, SpriteBatch _spriteBatch) 
        {
            gameInstance = _gameInstance;
            spriteBatch = _spriteBatch;
            systems.Add(new MovementSystem(this, typeof(MovementComponent)));
            systems.Add(new SpriteSystem(this, typeof(SpriteComponent)));
            systems.Add(new ControlSystem(this, typeof(ControlComponent)));
            gameInstance.controllers.Add(new PlayerController(gameInstance));
            AddPlayer(new Vector2(0, 0));
            mapGenerator = new MapGenerator(this);
        }

        public void AdaptiveScreenScale()
        {
            screenScale = gameInstance._graphics.PreferredBackBufferWidth / 1280 * 1;
        }
    }
}
    