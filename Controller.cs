using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
        private bool altEnterClickOccured = false;
        public Vector2 cameraLocation = new Vector2(0, 0);
        public int lastMouseWheelValue = 0;
        public MouseState mouseState;
        public float cameraZoom = 1;
        public Rectangle CameraFieldOfView {
            get {
                return new Rectangle(Convert.ToInt32((cameraLocation.X - gameInstance._graphics.PreferredBackBufferWidth / 2 / cameraZoom) ), 
                                   Convert.ToInt32((cameraLocation.Y - gameInstance._graphics.PreferredBackBufferHeight / 2 / cameraZoom) ), 
                                   Convert.ToInt32(gameInstance._graphics.PreferredBackBufferWidth / cameraZoom) + 10,
                                   Convert.ToInt32(gameInstance._graphics.PreferredBackBufferHeight / cameraZoom) + 10); 
            } 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cameraLocation = new Vector2(controllablePawn.location.X, controllablePawn.location.Y);

            mouseState = Mouse.GetState();

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
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                altEnterClickOccured = true;
            }
            else
            {
                if (altEnterClickOccured)
                {
                    gameInstance.SwitchFullScreenMode();
                    altEnterClickOccured = false;              //Полноэкранный режим
                }
            }

            //Управление на WASD

            if (Keyboard.GetState().IsKeyDown(Keys.A))      { controllablePawn.moveDirection.X = -1;}
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) { controllablePawn.moveDirection.X = 1; }
            else                                            { controllablePawn.moveDirection.X = 0; }

            if (Keyboard.GetState().IsKeyDown(Keys.S))      { controllablePawn.moveDirection.Y = -1;}
            else if (Keyboard.GetState().IsKeyDown(Keys.W)) { controllablePawn.moveDirection.Y = 1; }
            else                                            { controllablePawn.moveDirection.Y = 0; }

            if (mouseState.ScrollWheelValue > lastMouseWheelValue) //Колёсико мыши вверх
            {
                cameraZoom = cameraZoom * 1.1f;
            }
            else if (mouseState.ScrollWheelValue < lastMouseWheelValue) //Колёсико мыши вниз
            {
                cameraZoom = cameraZoom / 1.1f;
            }
            lastMouseWheelValue = mouseState.ScrollWheelValue;
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
            lastMouseWheelValue = mouseState.ScrollWheelValue;
        }
    }
}
