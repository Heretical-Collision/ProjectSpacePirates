using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS
{
    public class Entity
    {
        public List<BaseComponent> components = new List<BaseComponent>();
        
        public void AddComponent(BaseComponent _component)
        {
            components.Add(_component);
            _component.entityOfComponent = this;
        }

        public void AddComponents(List<BaseComponent> _components)
        {
            components.AddRange(_components);
            foreach (BaseComponent c in _components) c.entityOfComponent = this;
        }

        public Entity(List<BaseComponent> _components) 
        {
            AddComponents(_components);
        }

        public Entity()
        {

        }
    }
}
