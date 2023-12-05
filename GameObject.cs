﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject
{
    public class GameObject
    {
        public Vector2 location;
        public float angle = 0;
        public SpriteData spriteData;
        public GameWorld world;

        public GameObject(Vector2 _location, SpriteData _spriteData, GameWorld _world)
        {
            location = _location;
            spriteData = _spriteData;
            world = _world;
        }
    }
}
