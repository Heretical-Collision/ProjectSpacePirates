﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS.Sprite
{
    public class SpriteComponent : BaseComponent
    {
        public SpriteData spriteData;
        public Vector2 spriteOffset = new Vector2(0, 0);
        public float spriteLayer = 0;
        public float spriteAngle = 0;

        public SpriteComponent(SpriteData _spriteData, Vector2 _spriteOffset, float _spriteLayer) 
        {
            spriteData = _spriteData;
            spriteOffset = _spriteOffset;
            spriteLayer = _spriteLayer;
        }
    }
}