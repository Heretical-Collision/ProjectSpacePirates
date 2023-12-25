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
        private Vector2 lastFrameMoveDirection = new Vector2(0, 0);

        public override void Update(GameTime gameTime)
        {
            if (lastFrameMoveDirection != moveDirection)
            {
                if (moveDirection.X == 1)       { spriteData.SwitchAnimation(0); spriteData.SwitchAnimationPause(false); }
                else if (moveDirection.X == -1) { spriteData.SwitchAnimation(2); spriteData.SwitchAnimationPause(false); }
                else if (moveDirection.Y == 1)  { spriteData.SwitchAnimation(3); spriteData.SwitchAnimationPause(false); }
                else if (moveDirection.Y == -1) { spriteData.SwitchAnimation(1); spriteData.SwitchAnimationPause(false); }
                else { spriteData.SwitchAnimationPause(true); spriteData.ResetAnimation(); }
            }   
            lastFrameMoveDirection = moveDirection;
            base.Update(gameTime);
        }
        public PlayerCharacter(Vector2 _location, SpriteData _spriteData, GameWorld _world, PlayerController _controller, float _layer) : base(_location, _spriteData, _world, _controller, _layer)
        {
            location = _location;
            spriteData = _spriteData;
            world = _world;
            controller = _controller;
            layer = _layer;
            ((PlayerController)controller).controllablePawn = this;
        }
    }
}
