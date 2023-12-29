using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS.Control
{
    public class ControlSystem : BaseSystem
    {
        protected override bool isTickable { get { return false; } }

        static public void ConnectControllerToComponent(ControlComponent _component, Controller _controller)
        {
            _component.controller = _controller;
            _controller.controllableEntity = _component.entityOfComponent;
        }

        static public void ChangeMoveDirection(ControlComponent _component, float x, float y)
        {
            _component.moveDirection.X = x;
            _component.moveDirection.Y = y;
        }

        public ControlSystem(GameWorld _gameWorld, Type _typeOfComponent) : base(_gameWorld, _typeOfComponent)
        {

        }
    }
}
