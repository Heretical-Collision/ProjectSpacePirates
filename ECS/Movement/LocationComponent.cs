using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS.Movement;

public class LocationComponent : BaseComponent
{
    public Vector2 location = new Vector2(0, 0);



    public LocationComponent(Vector2 _location)
    {
        location.X = _location.X;
        location.Y = -_location.Y;
    }
}
