using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ProjectSpaceProject.ECS.Control;

namespace ProjectSpaceProject.ECS.Movement
{
    public class MovementSystem : BaseSystem
    {
        protected override bool isTickable { get { return true; } }

        public override void Run()
        {
            base.Run();

        }

        public override void Update()
        {
            base.Update();
            
        }

        protected override void ComponentTick(Entity entity, BaseComponent component)
        {
            base.ComponentTick(entity, component);
            (component as MovementComponent).location.X += (component as MovementComponent).velocity.X;
            (component as MovementComponent).location.Y += -(component as MovementComponent).velocity.Y;
            ControlComponent inputComponent = GetComponentOfEntity(entity, typeof(ControlComponent)) as ControlComponent;
            if (inputComponent is not null)
            {
                if (inputComponent.moveDirection.X != 0 || inputComponent.moveDirection.Y != 0)
                {
                    Vector2 normalizedInput = Vector2.Normalize(inputComponent.moveDirection);
                    (component as MovementComponent).location.X += normalizedInput.X * (component as MovementComponent).movementSpeed;
                    (component as MovementComponent).location.Y += -normalizedInput.Y * (component as MovementComponent).movementSpeed;
                }
            }
        }

        public MovementSystem(GameWorld _gameWorld, Type _typeOfComponent) : base(_gameWorld, _typeOfComponent) 
        {

        }
    }
}
