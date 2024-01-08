using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS
{
    public abstract class BaseSystem
    {
        protected GameWorld gameWorld;
        public Type typeOfComponent;
        protected virtual bool isTickable { get { return false; } } //Будет ли вызываться функция ComponentTick. Выключить, если нет необходимости в постоянном обновлении компонентов
        protected int componentID = 0;

        public virtual void Run()
        {

        }
        public virtual void Update()
        {
            if (isTickable)
            {
                foreach (Entity entity in gameWorld.entitiesBySystems[this.GetType()])
                {
                    ComponentTick(entity);
                }
            }
        }

        protected virtual void ComponentTick(Entity entity)
        {

        }

        protected BaseComponent ComponentOfEntity(Entity entity)
        {
            return GetComponentOfEntity(entity, typeOfComponent, componentID);
        }

        static public BaseComponent GetComponentOfEntity(Entity entity, Type _typeOfComponent, int _componentID)
        {
            return entity.components[_componentID];
        }


        protected BaseSystem(GameWorld _gameWorld, Type _typeOfComponent) 
        {   
            gameWorld = _gameWorld;
            typeOfComponent = _typeOfComponent;
            componentID = Array.IndexOf(gameWorld.componentsTypesID, _typeOfComponent);
            Run();
        }
    }
}
