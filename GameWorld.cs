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
        public float screenScale = 10;

        public void AddPlayer()
        {
            //            gameInstance.controllers.Add(new PlayerController(gameInstance));
            TickableObject tempObj = new PlayerCharacter(new Vector2(200, 200), new SpriteData(gameInstance.spriteList[1], 1, 4, 4), this);
            tickableObjects.Add(tempObj);
            gameObjects.Add(tempObj);
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
            spriteBatch.Begin();
            foreach (GameObject obj in gameObjects)
            {
                spriteBatch.Draw(
                    obj.spriteData.spriteAtlas, 
                    obj.location, //new Vector2(obj.location.X-), 
                    obj.spriteData.sourceRectangleOfFrame, //new Rectangle(0, 0, obj.spriteData.currentSprite.Width, obj.spriteData.currentSprite.Height) 
                    Color.White, 
                    obj.angle,
                    //new Vector2(obj.spriteData.currentColumn * obj.spriteData.widthOfFrame + obj.spriteData.widthOfFrame  / 2, obj.spriteData.currentRow * obj.spriteData.heightOfFrame + obj.spriteData.heightOfFrame / 2), 
                    new Vector2(0, 0),
                    screenScale, 
                    SpriteEffects.None, 
                    1);

                //if (obj.isSelected) spriteBatch.Draw(obj.sprite, obj.location, new Rectangle(0, 0, obj.sprite.Width, obj.sprite.Height), Color.Green, obj.angle, new Vector2(obj.sprite.Width / 2, obj.sprite.Height / 2), screenScale*2, SpriteEffects.None, 1);

            }
            spriteBatch.End();
        }


        public GameWorld(GameI _gameInstance, SpriteBatch _spriteBatch) 
        {
            gameInstance = _gameInstance;
            spriteBatch = _spriteBatch;
            AddPlayer();
            //physicObjects.Add(new GameUnit(new Vector2(100, 100), new Vector2(0, 0), 0, gameInstance.spriteList[0], 3, true));
        }

    }
}
