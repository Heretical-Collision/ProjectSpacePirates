using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS.Collision;
using ProjectSpaceProject.ECS.Control;
using ProjectSpaceProject.EventManage;
using static ProjectSpaceProject.StaticECSMethods;

namespace ProjectSpaceProject.ECS.Movement;

[SystemsOrder("PostPhysics")]
public class MovementSystem : BaseSystem
{
    public override void Initialize()
    {
        base.Initialize();
        gameWorld.eventManager.SubscribeEvent<MovementComponent, EventOnCollideArgs>(OnCollide);
    }

    public override void Update()
    {
        base.Update();
        MovementComponent[] movementComponents = GetAllComponents<MovementComponent>().ToArray();
        List<LocationComponent> locationComponentsList = new List<LocationComponent>();
        List<CollisionComponent> collisionComponentsList = new List<CollisionComponent>();

        for (int i = 0; i < movementComponents.Length; i++)
        {
            locationComponentsList.Add(GetComponentOfEntity<LocationComponent>(movementComponents[i].entityOfComponent));
            collisionComponentsList.Add(GetComponentOfEntity<CollisionComponent>(movementComponents[i].entityOfComponent));
        }

        LocationComponent[] locationComponents = locationComponentsList.ToArray();
        CollisionComponent[] collisionComponents = collisionComponentsList.ToArray();

        for (int i = 0; i < movementComponents.Length; i++)
        {
            ControlComponent inputComponent = GetComponentOfEntity<ControlComponent>(movementComponents[i].entityOfComponent);

            if (inputComponent is not null)
            {
                if (inputComponent.moveDirection.X != 0 || inputComponent.moveDirection.Y != 0)
                {
                    Vector2 normalizedInput = Vector2.Normalize(inputComponent.moveDirection);
                    movementComponents[i].velocity.X = normalizedInput.X * movementComponents[i].movementSpeed;
                    movementComponents[i].velocity.Y = normalizedInput.Y * movementComponents[i].movementSpeed;
                }
                else 
                {
                    movementComponents[i].velocity.X = 0;
                    movementComponents[i].velocity.Y = 0;
                }
            }
            if (collisionComponents[i] is not null)
            {
                Vector2 tempLoc = new Vector2(locationComponents[i].location.X, locationComponents[i].location.Y);
                locationComponents[i].location.X += movementComponents[i].velocity.X;
                if (CollisionSystem.IntersectWithAnything(collisionComponents[i], locationComponents[i])) locationComponents[i].location.X = tempLoc.X;

                locationComponents[i].location.Y += -movementComponents[i].velocity.Y;
                if (CollisionSystem.IntersectWithAnything(collisionComponents[i], locationComponents[i])) locationComponents[i].location.Y = tempLoc.Y;
            }
        }
    }

    void OnCollide(Entity entity, MovementComponent movementComponent, EventOnCollideArgs args)
    {
        CollisionComponent cc1 = (CollisionComponent)GetComponentOfEntityByType(args.first, typeof(CollisionComponent));
        CollisionComponent cc2 = (CollisionComponent)GetComponentOfEntityByType(args.second, typeof(CollisionComponent));
        LocationComponent lc1 = (LocationComponent)GetComponentOfEntityByType(args.first, typeof(LocationComponent));
        LocationComponent lc2 = (LocationComponent)GetComponentOfEntityByType(args.second, typeof(LocationComponent));
        Vector2 v1 = lc1.location + cc1.collisionOffset;
        Vector2 v2 = lc2.location + cc2.collisionOffset;

        if (cc1.collisionType == CollisionInteractionType.SoftBlock)
        {
            Vector2 direction = Vector2.Normalize(v1 - v2);
            lc1.location.X = lc1.location.X + direction.X / 10;
            lc1.location.Y = lc1.location.Y + direction.Y / 10;
        }

        if (cc2.collisionType == CollisionInteractionType.SoftBlock)
        {
            Vector2 direction = Vector2.Normalize(v2 - v1);
            lc2.location.X = lc2.location.X + direction.X / 10;
            lc2.location.Y = lc2.location.Y + direction.Y / 10;
        }
    }
}
