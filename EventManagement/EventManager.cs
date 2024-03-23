using ProjectSpaceProject.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ProjectSpaceProject.StaticECSMethods;

namespace ProjectSpaceProject.EventManage;

public class EventManager
{
    public delegate void EntityEventHandler<TComponent, TEvent>(Entity entityOrigin, TComponent component, TEvent e) where TComponent : BaseComponent where TEvent : EntityEventArgs;
    private Dictionary<Type, Dictionary<Type, Delegate>> events = new Dictionary<Type, Dictionary<Type, Delegate>>();

    public void SubscribeEvent<TComponent, TEvent>(EntityEventHandler<TComponent, TEvent> eventHandler) where TComponent : BaseComponent where TEvent : EntityEventArgs
    {
        if (!events.ContainsKey(typeof(TEvent)))
        {
            events[typeof(TEvent)] = new Dictionary<Type, Delegate>() { { typeof(TComponent), eventHandler } };
        }
        else
        {
            if (!events[typeof(TEvent)].ContainsKey(typeof(TComponent))) events[typeof(TEvent)][typeof(TComponent)] = eventHandler;
            else events[typeof(TEvent)][typeof(TComponent)] = Delegate.Combine(events[typeof(TEvent)][typeof(TComponent)], eventHandler);
        }
    }

    public void UnsubscribeEvent<TComponent, TEvent>(EntityEventHandler<TComponent, TEvent> eventHandler) where TComponent : BaseComponent where TEvent : EntityEventArgs
    {
        if (events.ContainsKey(typeof(TEvent)))
        {
            if(events[typeof(TEvent)].ContainsKey(typeof(TComponent)))
                events[typeof(TEvent)][typeof(TComponent)] = Delegate.Remove(events[typeof(TEvent)][typeof(TComponent)], eventHandler);
        }
    }

    public void RaiseEvent<TEvent>(Entity entity, TEvent e) where TEvent : EntityEventArgs
    {
        if (events.ContainsKey(typeof(TEvent)))
        {
            foreach(KeyValuePair<Type, Delegate> d in events[typeof(TEvent)])
            {
                if (d.Value != null)
                {
                    d.Value.DynamicInvoke(entity, GetComponentOfEntityByType(entity, d.Key), e); //НЕОБХОДИМО ИСПРАВИТЬ В ОБОЗРИМОМ БУДУЩЕМ, DynamicInvoke СЛИШКОМ РЕСУРСОЗАТРАТНЫЙ
                }
            }
        }
    }
}
