using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSpaceProject
{
    public class MapGenerator
    {
        private GameWorld gameWorldInstance;
        public MapGenerator(GameWorld _gameWorldInstance) 
        {
            gameWorldInstance = _gameWorldInstance;
            
            for(int i = 0; i < 500; i++) 
            {
                for(int j = 0; j < 500; j++) 
                    gameWorldInstance.gameObjects.Add(new GameObject(new Vector2(i * 16, j * 16), new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 0), gameWorldInstance, 0f));
            }
        }
    }
}
