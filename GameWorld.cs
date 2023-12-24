using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject
{
    public class GameWorld
    {
        public GameI gameInstance;
        public List<TickableObject> tickableObjects = new List<TickableObject>();   //Здесь объекты, которые имеют функцию Update
        public List<GameObject> gameObjects = new List<GameObject>();               //Каждый объект должен быть отрисован, но не каждый должен изменяться со временем.
        public SpriteBatch spriteBatch;                                             //В общем-то можно и наоборот, изменять, но не отрисовывать.
        public float screenScale = 3;
        public Matrix matrix;
        public PlayerController ClientPlayerController { get { return (gameInstance.controllers[0] as PlayerController); } }
        public MapGenerator mapGenerator;

        public void AddPlayer()
        {   
            TickableObject tempObj = new PlayerCharacter(
                new Vector2(20, 20), 
                new SpriteData(new List<Texture2D>() { 
                        gameInstance.spriteList["defaultchRight"], 
                        gameInstance.spriteList["defaultchDown"],
                        gameInstance.spriteList["defaultchLeft"], 
                        gameInstance.spriteList["defaultchUp"]},
                    new List<int>() { 1, 1, 1, 1 },
                    new List<int>() { 6, 6, 6, 6 },
                    new List<int>() { 6, 6, 6, 6 }),
                this,
                new PlayerController(gameInstance));
            gameInstance.controllers.Add((tempObj as PlayerCharacter).controller);
            tickableObjects.Add(tempObj);
            gameObjects.Add(tempObj);
            ((tempObj as PlayerCharacter).controller as PlayerController).cameraLocation = new Vector2(tempObj.location.X, tempObj.location.Y);
        }

        public void Update(GameTime gameTime)
        {
            foreach (TickableObject obj in tickableObjects)
            {
                obj.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach (GameObject obj in gameObjects)
            {
                spriteBatch.Draw(
                    obj.spriteData.CurrentSpriteAtlas, 
                    (obj.location /*- ClientPlayerController.cameraLocation*/) * screenScale,
                    obj.spriteData.sourceRectangleOfFrame,
                    Color.White, 
                    obj.angle,
                    new Vector2(obj.spriteData.widthOfFrame / 2, obj.spriteData.heightOfFrame / 2),
                    screenScale, 
                    SpriteEffects.None, 
                    1);

            }
            spriteBatch.End();
        }


        public GameWorld(GameI _gameInstance, SpriteBatch _spriteBatch) 
        {
            gameInstance = _gameInstance;
            spriteBatch = _spriteBatch;
            AddPlayer();
            mapGenerator = new MapGenerator(this);
            //AdaptiveScreenScale();
        }

        public void AdaptiveScreenScale()
        {
            screenScale = gameInstance._graphics.PreferredBackBufferWidth / 1280 * 3;
        }
    }
}
