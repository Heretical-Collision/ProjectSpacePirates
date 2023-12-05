using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ProjectSpaceProject
{
    public class PlayerCharacter : ControllableEntity
    {


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public PlayerCharacter(Vector2 _location, SpriteData _spriteData, GameWorld _world) : base(_location, _spriteData, _world)
        {
            location = _location;
            spriteData = _spriteData;
            world = _world;
            controller = new PlayerController();
            ((PlayerController)controller).controllablePawn = this;
            world.gameInstance.controllers.Add(controller);
        }
    }
}
