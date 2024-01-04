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
        //public List<TickableObject> tickableObjects = new List<TickableObject>();   //Здесь объекты, которые имеют функцию Update
        //public List<GameObject> gameObjects = new List<GameObject>();               //Каждый объект должен быть отрисован, но не каждый должен изменяться со временем.
        public SpriteBatch spriteBatch;                                             //В общем-то можно и наоборот, изменять, но не отрисовывать.
        public float screenScale = 1;
        public Matrix matrix;
        public PlayerController ClientPlayerController { get { return (gameInstance.controllers.Count > 0) ? (gameInstance.controllers[0] as PlayerController) : null; } }
        public MapGenerator mapGenerator;
        public List<Entity> entities = new List<Entity>();
        public List<BaseSystem> systems = new List<BaseSystem>();

        public void AddPlayer(Vector2 location)
        {
            /*TickableObject tempObj = new PlayerCharacter(
                location, 
                new SpriteData(new List<Texture2D>() { 
                        gameInstance.spriteList["playerCharacterRight"], 
                        gameInstance.spriteList["playerCharacterDown"],
                        gameInstance.spriteList["playerCharacterLeft"], 
                        gameInstance.spriteList["playerCharacterUp"]},
                    new List<int>() { 1, 1, 1, 1 },
                    new List<int>() { 6, 6, 6, 6 },
                    new List<int>() { 6, 6, 6, 6 }),
                this,
                new PlayerController(gameInstance), 
                1f);
            gameInstance.controllers.Add((tempObj as PlayerCharacter).controller);
            tickableObjects.Add(tempObj);
            gameObjects.Add(tempObj);
            ((tempObj as PlayerCharacter).controller as PlayerController).cameraLocation = new Vector2(tempObj.location.X, tempObj.location.Y);*/
            ControlComponent tempRefControlComponent = new ControlComponent();
            entities.Add(new Entity(new List<BaseComponent>() {
                                        new MovementComponent(new Vector2(50, 50), new Vector2(0, 0), 1.5f), 
                                        new SpriteComponent(new SpriteData(new List<Texture2D>() { 
                                                    gameInstance.spriteList["playerCharacterRight"], 
                                                    gameInstance.spriteList["playerCharacterDown"],
                                                    gameInstance.spriteList["playerCharacterLeft"], 
                                                    gameInstance.spriteList["playerCharacterUp"]},
                                                new List<int>() { 1, 1, 1, 1 },
                                                new List<int>() { 6, 6, 6, 6 },
                                                new List<int>() { 6, 6, 6, 6 }),
                                            new Vector2(0, 0),
                                            1f),
                                        tempRefControlComponent}));
            ControlSystem.ConnectControllerToComponent(tempRefControlComponent, gameInstance.controllers[0]);
        }

        public void Update(GameTime gameTime)
        {
            /*foreach (TickableObject obj in tickableObjects)
            {
                obj.Update(gameTime);
            }*/

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

            /*foreach (GameObject obj in gameObjects)
            {
                // Объекты не должны отрисовываться, если они за кадром 
                if (new Rectangle(Convert.ToInt32(obj.location.X),
                                  Convert.ToInt32(obj.location.Y),
                                  obj.spriteData.widthOfFrame,
                                  obj.spriteData.heightOfFrame).Intersects(ClientPlayerController.CameraFieldOfView))
                {
                    spriteBatch.Draw(
                        obj.spriteData.CurrentSpriteAtlas,
                        obj.location,
                        obj.spriteData.sourceRectangleOfFrame,
                        Color.White,
                        obj.angle,
                        new Vector2(obj.spriteData.widthOfFrame / 2, obj.spriteData.heightOfFrame / 2),
                        1f,
                        SpriteEffects.None,
                        obj.layer);
                }
            }*/
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
    