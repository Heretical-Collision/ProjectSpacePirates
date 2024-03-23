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
using static ProjectSpaceProject.StaticECSMethods;

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
                    SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(i * 16, j * 16)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 0) } }, new Vector2(0, 0), 0f), /*new CollisionComponent(new Circle(8))*/});
            }
            //for (int i = 0; i < 20; i++) for (int j = 0; j < 20; j++)
                    //SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(i * 64, -64 - j * 64)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 1) } }, new Vector2(0, 0), 0f), new CollisionComponent(new Rectangle(0, 0, 16, 16), false), /*new MovementComponent(new Vector2((float)new Random().NextDouble() - 0.5f, (float)new Random().NextDouble() - 0.5f))*/ });
           // for (int i = 0; i < 30; i++)
                    //SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(-128 - i * 16, -128)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 1) } }, new Vector2(0, 0), 0f), new CollisionComponent(new Rectangle(0, 0, 16, 16), false) });
            for (int i = 1; i < 2; i++)
                    SpawnEntity(new List<BaseComponent>() { new LocationComponent(new Vector2(-128-64, -128 + i * 16)), new SpriteComponent(new Dictionary<string, SpriteData>() { { "tileAtlas1", new SpriteData(gameWorldInstance.gameInstance.spriteList["tilesAtlas1"], 3, 9, 0, 1) } }, new Vector2(0, 0), 0f), new CollisionComponent(new Rectangle(0, 0, 16, 16), false, CollisionInteractionType.Block, new Vector2(0, 0)) });

        }
    }
}
