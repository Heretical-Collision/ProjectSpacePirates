﻿using ProjectSpaceProject.ECS.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS.Movement;
using System.Drawing;
using static ProjectSpaceProject.StaticECSMethods;

namespace ProjectSpaceProject.ECS.Control
{
    public class ControlSystem : BaseSystem
    {
        public override void Initialize()
        {
            base.Initialize();
            foreach (Controller c in gameWorld.gameInstance.controllers) c.controlSystem = this;
        }
        static public void ConnectControllerToComponent(ControlComponent _component, Controller _controller)
        {
            _component.controller = _controller;
            _controller.controlComponent = _component;
            _controller.locationComponent = GetComponentOfEntity<LocationComponent>(_component.entityOfComponent);
        }

        public void ChangeMoveDirection(ControlComponent _component, float x, float y)
        {
            _component.moveDirection.X = x;
            _component.moveDirection.Y = y;
            ChangeSpriteByMovement(_component.entityOfComponent, _component);
        }

        protected void ChangeSpriteByMovement(Entity entity, BaseComponent component)
        {
            if (component == null) return; //Без ControlComponent это не работает.

            Vector2 moveDirection = (component as ControlComponent).moveDirection;
            SpriteComponent spriteComponent =  GetComponentOfEntity<SpriteComponent>(entity);
            SpriteData spriteData = SpriteSystem.GetCurrentSprite(spriteComponent);

            if ((component as ControlComponent).lastFrameMoveDirection != moveDirection)
            {
                if (moveDirection.X == 1)       { SpriteSystem.SetCurrentSpriteName(spriteComponent, "WalkRight"); spriteData.SwitchAnimationPause(false); }
                else if (moveDirection.X == -1) { SpriteSystem.SetCurrentSpriteName(spriteComponent, "WalkLeft"); spriteData.SwitchAnimationPause(false); }
                else if (moveDirection.Y == 1)  { SpriteSystem.SetCurrentSpriteName(spriteComponent, "WalkUp"); spriteData.SwitchAnimationPause(false); }
                else if (moveDirection.Y == -1) { SpriteSystem.SetCurrentSpriteName(spriteComponent, "WalkDown"); spriteData.SwitchAnimationPause(false); }
                else { spriteData.SwitchAnimationPause(true); spriteData.ResetAnimation(); }
            }
            (component as ControlComponent).lastFrameMoveDirection = moveDirection;
        }
    }
}
