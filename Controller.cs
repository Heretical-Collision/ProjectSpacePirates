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
using ProjectSpaceProject.ECS;
using ProjectSpaceProject.ECS.Control;
using ProjectSpaceProject.ECS.Movement;

namespace ProjectSpaceProject
{
    public class Controller
    {

        protected GameI gameInstance;
        public Entity controllableEntity;
        public ControlComponent GetControlComponent { get { return BaseSystem.GetComponentOfEntity(controllableEntity, typeof(ControlComponent)) as ControlComponent; } }
        public MovementComponent GetMovementComponent { get { return BaseSystem.GetComponentOfEntity(controllableEntity, typeof(MovementComponent)) as MovementComponent; } }

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
        private Dictionary<Keys, bool> keyboardButtonOccured = new Dictionary<Keys, bool>();
        public delegate void KeyPressedDelegate(Keys keyboardKey);
        public delegate void KeyReleasedDelegate(Keys keyboardKey);
        public KeyPressedDelegate KeyPressedEvent;
        public KeyReleasedDelegate KeyReleasedEvent;
        public Vector2 cameraLocation = new Vector2(0, 0);
        private int lastMouseWheelValue = 0;
        public MouseState mouseState;
        public float cameraZoom = 3;
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
            bool EntityIsValid = controllableEntity is not null && GetControlComponent is not null && GetMovementComponent is not null;
            if (EntityIsValid) cameraLocation = new Vector2(GetMovementComponent.location.X, GetMovementComponent.location.Y);

            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            mouseState = Mouse.GetState();

            foreach(KeyValuePair<Keys, bool> _key in keyboardButtonOccured)
            {
                if (pressedKeys.Contains(_key.Key))
                {
                    if (!keyboardButtonOccured[_key.Key]) KeyboardKeyPressed(_key.Key);
                    keyboardButtonOccured[_key.Key] = true;
                }
                else 
                {
                    if (_key.Value)
                    {
                        KeyboardKeyReleased(_key.Key);
                        keyboardButtonOccured[_key.Key] = false;
                    }
                }
            }

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
            Vector2 moveDirection = new Vector2(0,0);
            if (EntityIsValid)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))      { moveDirection.X = -1;}
                else if (Keyboard.GetState().IsKeyDown(Keys.D)) { moveDirection.X = 1; }
                else                                            { moveDirection.X = 0; }

                if  (Keyboard.GetState().IsKeyDown(Keys.S))     { moveDirection.Y = -1;}
                else if (Keyboard.GetState().IsKeyDown(Keys.W)) { moveDirection.Y = 1; }
                else                                            { moveDirection.Y = 0; }

                ControlSystem.ChangeMoveDirection(GetControlComponent, moveDirection.X, moveDirection.Y);
            }


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

        public void KeyboardKeyPressed(Keys key) //Вызывается, когда клавиша была нажата (один раз)
        {
            if (KeyPressedEvent is not null) KeyPressedEvent(key);
        }

        public void KeyboardKeyReleased(Keys key) //Вызывается, когда клавиша была отпущена
        {
            if (KeyPressedEvent is not null) KeyReleasedEvent(key);
        }


        public PlayerController(GameI _gameInstance) : base(_gameInstance)
        {
            gameInstance = _gameInstance;
            lastMouseWheelValue = mouseState.ScrollWheelValue;
            foreach (Keys key in (Keys[])Enum.GetValues(typeof(Keys))) //добавляет все клавиши в словарь. Массив Keys[] немного ускоряет цикл перечисления.
                keyboardButtonOccured.Add(key, false);
        }
    }
}
