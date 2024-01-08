using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS
{
    public class Entity
    {
        public BaseComponent[] components;
        public Type[] typesArray;
        
        public void AddComponent(BaseComponent _component)
        {
            components[Array.IndexOf(typesArray, _component.GetType())] = _component;
            _component.entityOfComponent = this;
            
        }

        public void AddComponents(List<BaseComponent> _components)
        {   
            foreach(BaseComponent component in _components)
                AddComponent(component);
        }

        public Entity(List<BaseComponent> _components, Type[] _typesArray) 
        {
            typesArray = _typesArray;
            components = new BaseComponent[typesArray.Length];
            AddComponents(_components);
        }

        public Entity(Type[] _typesArray)
        {
            typesArray = _typesArray;
            components = new BaseComponent[typesArray.Length];
        }
    }
}
