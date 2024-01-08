using ProjectSpaceProject.ECS.Movement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectSpaceProject.ECS.Collision
{
    public class CollisionSystem : BaseSystem
    {
        protected override bool isTickable { get { return true; } }
        protected int movementComponentID;

        public override void Update()
        {
            base.Update();
        }

        protected override void ComponentTick(Entity entity)
        {
            CollisionComponent component = ComponentOfEntity(entity) as CollisionComponent;
            Circle circle1 = new Circle((GetComponentOfEntity(entity, typeof(MovementComponent), movementComponentID) as MovementComponent).location, component.circleCollision.radius);

            foreach (Entity e in gameWorld.entitiesBySystems[this.GetType()])
            {
                if (e == entity) continue;
                CollisionComponent collision = ComponentOfEntity(e) as CollisionComponent;
                if (collision is null) continue;

                Circle circle2 = new Circle((GetComponentOfEntity(e, typeof(MovementComponent), movementComponentID) as MovementComponent).location, collision.circleCollision.radius);
                if (circle1.Intersects(circle2))
                {
                    Collide(component, collision);
                }
            }
        }
        public void Collide(CollisionComponent collision1, CollisionComponent collision2)
        {
            (GetComponentOfEntity(collision1.entityOfComponent, typeof(MovementComponent), movementComponentID) as MovementComponent).location = new Vector2(0, 0);
        }

        public CollisionSystem(GameWorld _gameWorld, Type _typeOfComponent) : base(_gameWorld, _typeOfComponent)
        {
            movementComponentID = Array.IndexOf(gameWorld.componentsTypesID, typeof(MovementComponent));
        }
    }
}
