using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS.Movement;

public class MovementComponent : BaseComponent
{
    public Vector2 velocity = new Vector2(0, 0);
    public float movementSpeed = 1;


    public MovementComponent(Vector2 _velocity, float speed)
    {
        velocity = _velocity;
        movementSpeed = speed;
    }

    public MovementComponent(Vector2 _velocity)
    {
        velocity = _velocity;
    }
}
