using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS.Camera
{
    public class CameraSystem : BaseSystem
    {

        public CameraSystem(GameWorld _gameWorld, Type _typeOfComponent) : base(_gameWorld, _typeOfComponent)
        {

        }
    }
}
