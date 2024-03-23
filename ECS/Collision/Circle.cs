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

        public bool IntersectsWithRectangle(Rectangle rectangle)
        {
            Vector2 closestP = ClosestPoint(rectangle);
            float distanceX = x - closestP.X;
            float distanceY = y - closestP.Y;
            float distanceSquared = (float)Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));
            return distanceSquared <= (radius);
        }

        private Vector2 ClosestPoint(Rectangle rectangle)
        {
            return new Vector2(Math.Max(rectangle.X, Math.Min(rectangle.X + rectangle.Width, x)),
                               Math.Max(-rectangle.Y, Math.Min(-rectangle.Y + rectangle.Height, y)));
        }

        static public bool IntersectsStatic(float x1, float y1, float r1, float x2, float y2, float r2)
        {
            float mX = (x1 - x2);
            float mY = (y1 - y2);
            float sR = r1 + r2;
            return (mX * mX + mY * mY) < sR * sR;
        }

        static public bool IntersectsWithRectangleStatic(float x1, float y1, float r, float x2, float y2, float w, float h)
        {
            Vector2 closestP = ClosestPointStatic(x1, y1, x2, y2, w, h);
            float distanceX = x1 - closestP.X;
            float distanceY = y1 - closestP.Y;
            float distanceSquared = (float)Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));
            return distanceSquared <= (r);
        }

        static private Vector2 ClosestPointStatic(float x1, float y1, float x2, float y2, float w, float h)
        {
            return new Vector2(Math.Max(x2, Math.Min(x2 + w, x1)),
                               Math.Max(y2, Math.Min(y2 + h, y1)));
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
            y = -_y;
            radius = _radius;
        }

        public Circle(Vector2 _loc, float _radius)
        {
            x = _loc.X;
            y = -_loc.Y;
            radius = _radius;
        }

        public Rectangle CircleToRectangle() 
        {
            return new Rectangle((int)x, (int)y, (int)radius / 2, (int)radius / 2);
        }
    }
}
