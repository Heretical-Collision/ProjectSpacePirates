﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject
{
    public class Controller
    {

        protected GameI gameInstance;
        public ControllableEntity controllablePawn;

        public Controller(GameI _gameInstance)
        {
            gameInstance = _gameInstance;

        }

        virtual public void Update(GameTime gameTime)
        {

        }

    }



    public class PlayerController : Controller
    {

        private bool leftClickOccured = false;
        private bool rightClickOccured = false;
        private bool enterClickOccured = false;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MouseState mouseState = Mouse.GetState();

            //Проверить, нажата ли левая кнопка мыши
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                leftClickOccured = true;
            }
            else
            {
                if (leftClickOccured) //Данный блок вызывается только при отпускании левой кнопки мыши, один раз (событие клика)
                {
                    LeftMouseClick(mouseState.X, mouseState.Y);
                    leftClickOccured = false;
                }
            }

            //Проверить, нажата ли левая правая мыши
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                rightClickOccured = true;
            }
            else
            {
                if (rightClickOccured) //Данный блок вызывается только при отпускании правой кнопки мыши, один раз (событие клика)
                {
                    RightMouseClick(mouseState.X, mouseState.Y);
                    rightClickOccured = false;
                }
            }

            //Проверить, нажат ли Enter
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                enterClickOccured = true;
            }
            else
            {
                if (enterClickOccured)
                {
                    gameInstance.SwitchFullScreenMode();
                    enterClickOccured = false;              //Полноэкранный режим
                }
            }

            //Управление на WASD

            if (Keyboard.GetState().IsKeyDown(Keys.A)) { controllablePawn.moveDirection.X = -1; }
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) { controllablePawn.moveDirection.X = 1; }
            else { controllablePawn.moveDirection.X = 0; }

            if (Keyboard.GetState().IsKeyDown(Keys.S)) { controllablePawn.moveDirection.Y = -1; }
            else if (Keyboard.GetState().IsKeyDown(Keys.W)) { controllablePawn.moveDirection.Y = 1; }
            else { controllablePawn.moveDirection.Y = 0; }

        }

        public void LeftMouseClick(int x, int y)
        {

        }

        public void RightMouseClick(int x, int y)
        {

        }

        public PlayerController(GameI _gameInstance) : base(_gameInstance)
        {
            gameInstance = _gameInstance;
        }
    }
}
