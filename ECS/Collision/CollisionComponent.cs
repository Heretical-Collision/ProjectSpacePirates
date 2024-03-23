using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectSpaceProject.ECS.Collision;

public class CollisionComponent : BaseComponent
{
    public Rectangle rectangleCollision;
    public Circle circleCollision;
    public bool isStatic = true;
    public CollisionInteractionType collisionType;
    public Vector2 collisionOffset;

    public CollisionComponent(Circle _circleCollision, CollisionInteractionType _collisionType, Vector2 _collisionOffset)
    {
        circleCollision = _circleCollision;
        collisionType = _collisionType;
        collisionOffset = _collisionOffset;
    }

    public CollisionComponent(Rectangle _rectangleCollision, CollisionInteractionType _collisionType, Vector2 _collisionOffset)
    {
        rectangleCollision = _rectangleCollision;
        collisionType = _collisionType;
        collisionOffset = _collisionOffset;
        collisionOffset.Y += rectangleCollision.Height;

    }

    public CollisionComponent(Circle _circleCollision, bool _isStatic, CollisionInteractionType _collisionType, Vector2 _collisionOffset)
    {
        circleCollision = _circleCollision;
        isStatic = _isStatic;
        collisionType = _collisionType;
        collisionOffset = _collisionOffset;
    }

    public CollisionComponent(Rectangle _rectangleCollision, bool _isStatic, CollisionInteractionType _collisionType, Vector2 _collisionOffset)
    {
        rectangleCollision = _rectangleCollision;
        isStatic = _isStatic;
        collisionType = _collisionType;
        collisionOffset = _collisionOffset;
        collisionOffset.Y += rectangleCollision.Height;
    }
}
