using ProjectSpaceProject.ECS.Collision;
using ProjectSpaceProject.ECS.Control;
using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.ECS.Sprite;
using ProjectSpaceProject.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.ComponentModel;

namespace ProjectSpaceProject
{
    static public partial class StaticECSMethods
    {
        static public GameWorld gameWorld;
        static public Dictionary<Type, int> componentIndices = new Dictionary<Type, int>();
        static public void SpawnEntity(List<BaseComponent> components)
        {
            Entity tempEntity = new Entity();
            tempEntity.components = new BaseComponent[gameWorld.componentsByType.Count];
            AttachComponentsToEntity(tempEntity, components);
            gameWorld.entities.Add(tempEntity);
            foreach (BaseComponent component in components)
            {
                gameWorld.componentsByType[component.GetType()].Item1.Add(component);
            }
        }

        static public void AttachComponentsToEntity(Entity entity, List<BaseComponent> components)
        {
            foreach (BaseComponent component in components)
            {
                AttachComponentToEntity(entity, component);
            }
        }

        static public void AttachComponentToEntity(Entity entity, BaseComponent component)
        {
            entity.components[GetComponentIDbyObject(component)] = component;
            component.entityOfComponent = entity;
        }

        static public void AddPlayer(Vector2 location)
        {
            ControlComponent tempRefControlComponent = new ControlComponent();

            SpawnEntity(new List<BaseComponent>() {
                            new LocationComponent(location),
                            new MovementComponent(new Vector2(0, 0), 1.5f),
                            new SpriteComponent(new Dictionary<string, SpriteData>(){
                                               { "WalkRight", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterRight"], 1, 6, 6, 0)},
                                               { "WalkDown", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterDown"], 1, 6, 6, 0)},
                                               { "WalkLeft", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterLeft"], 1, 6, 6, 0)},
                                               { "WalkUp", new SpriteData(gameWorld.gameInstance.spriteList["playerCharacterUp"], 1, 6, 6, 0)}},
                                new Vector2(0, 0),
                                1f),
                            new CollisionComponent(new Circle(8), false, CollisionInteractionType.Block, new Vector2(8, -6)),
                            tempRefControlComponent});
            ControlSystem.ConnectControllerToComponent(tempRefControlComponent, gameWorld.gameInstance.controllers[0]);
        }

        static public List<T> GetAllComponents<T>() where T : BaseComponent
        {
            //return gameWorld.componentsByType[typeof(T)].Item1;
            return gameWorld.componentsByType[typeof(T)].Item1.Cast<T>().ToList();
        }

        static public BaseComponent GetComponentOfEntityByType(Entity entity, Type type)
        {
            return entity.components[componentIndices[type]];
        }

        static public T GetComponentOfEntity<T>(Entity entity) where T : BaseComponent
        {
            return (T)entity.components[componentIndices[typeof(T)]];
        }

        static public int GetComponentID<T>() where T : BaseComponent
        {
            return componentIndices[typeof(T)];
        }

        static public int GetComponentIDbyObject(BaseComponent component)
        {
            return componentIndices[component.GetType()];
        }

        static public void DestroyComponent(BaseComponent component)
        {
            if (component != null) 
            {
                component.entityOfComponent.components[GetComponentIDbyObject(component)] = null;
                gameWorld.componentsByType[component.GetType()].Item1.Remove(component);
            }
        }

        static public void DestroyEntity(Entity entity)
        {
            if (entity != null) 
            {
                foreach(BaseComponent bc in entity.components)
                {
                    DestroyComponent(bc);
                }
                gameWorld.entities.Remove(entity);
            }
        }

        static public void DestroyEntities(List<Entity> entities)
        {
            if (entities.Count < 1) return;

            Dictionary<Type, List<BaseComponent>> componentsByType = new Dictionary<Type, List<BaseComponent>>();

            foreach (Entity entity in entities)
            {
                foreach (BaseComponent component in entity.components)
                {
                    if (component is not null) 
                        if (!componentsByType.ContainsKey(component.GetType())) componentsByType.Add(component.GetType(), new List<BaseComponent>() { component });
                        else componentsByType[component.GetType()].Add(component);
                }
            }
            foreach (KeyValuePair<Type, List<BaseComponent>> t in componentsByType)
            {
                gameWorld.componentsByType[t.Key].Item1.RemoveAll(с => t.Value.Contains(с));
            }
            gameWorld.entities.RemoveAll(e => entities.Contains(e));
            gameWorld.serverManager.entitiesToDestroy.Clear();
        }

        static public void DestroyComponents(List<BaseComponent> components)
        {
            if (components.Count < 1) return;

            Dictionary<Type, List<BaseComponent>> componentsByType = new Dictionary<Type, List<BaseComponent>>();

            foreach (BaseComponent c in components)
            {
                c.entityOfComponent.components[GetComponentIDbyObject(c)] = null;
                componentsByType[c.GetType()].Add(c);
            }

            foreach (KeyValuePair<Type, List<BaseComponent>> t in componentsByType)
            {
                gameWorld.componentsByType[t.Key].Item1.RemoveAll(с => t.Value.Contains(с));
            }
            gameWorld.serverManager.componentsToDestroy.Clear();
        }
    }
}
