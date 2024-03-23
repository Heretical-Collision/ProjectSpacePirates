using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.EventManage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectSpaceProject.StaticECSMethods;

namespace ProjectSpaceProject.ECS.Collision;

[SystemsOrder("Physics", 1)]
public class CollisionProcessingSystem : BaseSystem
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Update()
    {
        base.Update();
        foreach (CollisionDataComponent cdc in GetAllComponents<CollisionDataComponent>().ToList())
        {
            gameWorld.eventManager.RaiseEvent<EventOnCollideArgs>(cdc.intersectedCollisionOrigin,
                                                                  new EventOnCollideArgs(cdc.intersectedCollisionOrigin, cdc.intersectedCollisionTarget));
            DestroyDataComponent(cdc); //Пересечения являются отдельной сущностью, создаваемой при их обнаружении и их необходимо удалять после обработки, иначе произойдет утечка памяти
        }
    }
        
    public void DestroyDataComponent(CollisionDataComponent cdc)
    {
        gameWorld.serverManager.entitiesToDestroy.Add(cdc.entityOfComponent);
    }
}
public sealed class EventOnCollideArgs : EntityEventArgs
{
    public Entity first;
    public Entity second;

    public EventOnCollideArgs(Entity first, Entity second)
    {
        this.first = first;
        this.second = second;
    }
}