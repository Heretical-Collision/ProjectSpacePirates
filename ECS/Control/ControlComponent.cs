using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectSpaceProject.ECS.Control
{
    public class ControlComponent : BaseComponent
    {
        public Vector2 lastFrameMoveDirection = new Vector2(0, 0);
        public Vector2 moveDirection = new Vector2(0, 0);
        public Controller controller = null;
    }
}
