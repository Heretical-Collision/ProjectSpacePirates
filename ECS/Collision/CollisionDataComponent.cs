using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectSpaceProject.ECS.Collision
{
    public class CollisionDataComponent : BaseComponent
    {
        public Entity intersectedCollisionOrigin;
        public Entity intersectedCollisionTarget;

        public CollisionDataComponent(Entity _intersectedCollisionOrigin, Entity _intersectedCollisionTarget)
        {
            intersectedCollisionOrigin = _intersectedCollisionOrigin;
            intersectedCollisionTarget = _intersectedCollisionTarget;
        }
    }
}
