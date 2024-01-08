using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS.Control;

namespace ProjectSpaceProject.ECS.Movement
{
    public class MovementSystem : BaseSystem
    {
        protected override bool isTickable { get { return true; } }
        protected int controlComponentID;

        public override void Run()
        {
            base.Run();

        }

        public override void Update()
        {
            base.Update();
            
        }

        protected override void ComponentTick(Entity entity)
        {
            base.ComponentTick(entity);
            MovementComponent component = ComponentOfEntity(entity) as MovementComponent;
            component.location.X += component.velocity.X;
            component.location.Y += -component.velocity.Y;
            ControlComponent inputComponent = GetComponentOfEntity(entity, typeof(ControlComponent), controlComponentID) as ControlComponent;
            if (inputComponent is not null)
            {
                if (inputComponent.moveDirection.X != 0 || inputComponent.moveDirection.Y != 0)
                {
                    Vector2 normalizedInput = Vector2.Normalize(inputComponent.moveDirection);
                    component.location.X += normalizedInput.X * component.movementSpeed;
                    component.location.Y += -normalizedInput.Y * component.movementSpeed;
                }
            }
        }

        public MovementSystem(GameWorld _gameWorld, Type _typeOfComponent) : base(_gameWorld, _typeOfComponent) 
        {
            controlComponentID = Array.IndexOf(gameWorld.componentsTypesID, typeof(ControlComponent));
        }
    }
}
