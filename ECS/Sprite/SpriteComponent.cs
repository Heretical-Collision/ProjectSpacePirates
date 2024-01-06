using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS.Sprite
{
    public class SpriteComponent : BaseComponent
    {
        public Dictionary<string, SpriteData> spriteData;
        public string currentSpriteName;
        public Vector2 spriteOffset = new Vector2(0, 0);
        public float spriteLayer = 0;
        public float spriteAngle = 0;
        public float spriteScale = 1;

        public SpriteComponent(Dictionary<string, SpriteData> _spriteData, Vector2 _spriteOffset, float _spriteLayer) 
        {
            spriteData = _spriteData;
            spriteOffset = _spriteOffset;
            spriteLayer = _spriteLayer;
            currentSpriteName = _spriteData.First().Key;
        }

        public SpriteComponent(Dictionary<string, SpriteData> _spriteData, Vector2 _spriteOffset, float _spriteLayer, float _spriteScale)
        {
            spriteData = _spriteData;
            spriteOffset = _spriteOffset;
            spriteLayer = _spriteLayer;
            spriteScale = _spriteScale;
            currentSpriteName = _spriteData.First().Key;
        }
    }
}
