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
    public class TickableObject : GameObject
    {
        public Vector2 velocity = Vector2.Zero;
        public float sizeRadius = 0;
        public bool isSelectable = false;
        public bool isSelected = false;
        public float mass = 1;

        virtual public void Update(GameTime gameTime)
        {
            location.X = location.X + velocity.X;
            location.Y = location.Y - velocity.Y;
            spriteData.Update();
        }

        virtual public void AddImpulse(Vector2 impulse)
        {
            velocity = velocity + impulse / mass;
        }

        public TickableObject(Vector2 _location, SpriteData _spriteData, GameWorld _world, float _layer) : base(_location, _spriteData, _world, _layer)
        {
            location = _location;
            spriteData = _spriteData;
            world = _world;
            layer = _layer;
        }
        
        override public void Destroy()
        {   
            world.tickableObjects.Remove(this);
            base.Destroy();
        }
    }

}
