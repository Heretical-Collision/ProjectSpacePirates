using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectSpaceProject.ECS.Collision
{
    public struct Circle
    {
        public float x;
        public float y;
        public float radius;

        public bool Intersects(Circle circle)
        {
            float mX = (x - circle.x);
            float mY = (y - circle.y);
            float sR = radius + circle.radius;
            return (mX * mX + mY * mY) < sR * sR;
        }

        public Circle(float _radius)
        {
            x = 0;
            y = 0;
            radius = _radius;
        }

        public Circle(float _x, float _y, float _radius)
        {
            x = _x;
            y = _y;
            radius = _radius;
        }

        public Circle(Vector2 _loc, float _radius)
        {
            x = _loc.X;
            y = _loc.Y;
            radius = _radius;
        }
    }
}
