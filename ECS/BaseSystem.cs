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
        protected Type typeOfComponent;
        protected virtual bool isTickable { get { return false; } } //Будет ли вызываться функция ComponentTick. Выключить, если нет необходимости в постоянном обновлении компонентов

        public virtual void Run()
        {

        }
        public virtual void Update()
        {
            if (isTickable) //Метод виртуальный. Ничего страшного, если компилятор говорит о недостижимости кода.
            {
                BaseComponent outComponent;
                foreach (Entity entity in gameWorld.entities)
                {
                    outComponent = ComponentOfEntity(entity);
                    if (outComponent is not null) ComponentTick(entity, outComponent);
                }
            }
        }

        protected virtual void ComponentTick(Entity entity, BaseComponent component)
        {

        }

        protected BaseComponent ComponentOfEntity(Entity entity)
        {
            foreach (BaseComponent comp in entity.components)
            {
                if (comp.GetSelfType == typeOfComponent) return comp;
            }
            return null;
        }

        static public BaseComponent GetComponentOfEntity(Entity entity, Type _typeOfComponent)
        {
            foreach (BaseComponent comp in entity.components)
            {
                if (comp.GetSelfType == _typeOfComponent) return comp;
            }
            return null;
        }

        protected BaseSystem(GameWorld _gameWorld, Type _typeOfComponent) 
        {   
            gameWorld = _gameWorld;
            typeOfComponent = _typeOfComponent;
            Run();
        }
    }
}
