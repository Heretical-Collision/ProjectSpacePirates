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
        public GameWorld gameWorld;
        public virtual void Initialize()
        {

        }
        public virtual void Update()
        {
        }


        protected BaseSystem() 
        {   

        }
    }
}
