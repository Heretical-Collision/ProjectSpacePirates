/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject
{
    public class GameObject
    {
        public Vector2 location;
        public float angle = 0;
        public SpriteData spriteData;
        public GameWorld world;
        public float layer = 0;

        public GameObject(Vector2 _location, SpriteData _spriteData, GameWorld _world, float _layer)
        {
            location = _location;
            spriteData = _spriteData;
            world = _world;
            layer = _layer;
        }

        virtual public void Destroy()
        {
            world.gameObjects.Remove(this);
        }
    }
}
*/