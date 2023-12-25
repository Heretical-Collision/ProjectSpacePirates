using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject
{
    public class ControllableEntity : TickableObject
    {   
        public Vector2 moveDirection = new Vector2(0, 0);
        public float speed = 2;
        public Controller controller;
        public int team = 1;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (moveDirection.X != 0 || moveDirection.Y != 0) velocity = Vector2.Normalize(moveDirection * 2) * speed;  //Нормализация нужна, потому что иначе при диагональном движении скорость будет больше, чем при вертикальном / горизонательном
            else velocity = velocity / 2;
        }

        public ControllableEntity(Vector2 _location, SpriteData _spriteData, GameWorld _world, Controller _controler, float _layer) : base(_location, _spriteData, _world, _layer)
        {
            location = _location;
            spriteData = _spriteData;
            world = _world;
            controller = _controler;
            layer = _layer;
            (controller).controllablePawn = this;
        }

    }
}
