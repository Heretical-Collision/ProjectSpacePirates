using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectSpaceProject.ECS;
using ProjectSpaceProject.ECS.Collision;
using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.ECS.Sprite;

namespace ProjectSpaceProject
{
    public class MapGenerator
    {
        private GameWorld gameWorldInstance;
        public MapGenerator(GameWorld _gameWorldInstance) 
        {
            gameWorldInstance = _gameWorldInstance;

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                    gameWorldInstance.SpawnEntity(new List<BaseComponent>() { new MovementComponent(new Vector2(i * 16, -j * 16)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 0) } }, new Vector2(0, 0), 0f)});
            }
            for(int i = 0; i < 26; i++) for (int j = 0; j < 26; j++)
                    gameWorldInstance.SpawnEntity(new List<BaseComponent>() { new MovementComponent(new Vector2(-32 + i * 16, 32 + j * 16)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 1) } }, new Vector2(0, 0), 0f), new CollisionComponent(new Circle(8)) });
        }
    }
}
