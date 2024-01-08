using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectSpaceProject.ECS.Collision
{
    public class CollisionComponent : BaseComponent
    {
        public Rectangle rectangleCollision;
        public Circle circleCollision;

        public CollisionComponent(Circle _circleCollision)
        {
            circleCollision = _circleCollision;
        }

        public CollisionComponent(Rectangle rectangleCollision)
        {
            this.rectangleCollision = rectangleCollision;
        }

    }
}
