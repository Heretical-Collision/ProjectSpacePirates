using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS.Movement;

namespace ProjectSpaceProject.ECS.Sprite
{
    public class SpriteSystem : BaseSystem
    {
        protected override bool isTickable { get { return true; } }

        public override void Run()
        {
            base.Run();

        }

        public override void Update()
        {
            base.Update();
            

        }

        protected override void ComponentTick(Entity entity, BaseComponent component)
        {
            base.ComponentTick(entity, component);
            (component as SpriteComponent).spriteData.Update();
        }

        public void Draw()
        {
            foreach (Entity entity in gameWorld.entities)
            {
                Vector2 spriteLocation = new Vector2(0, 0);
                foreach (BaseComponent component in entity.components)
                {
                    if (component.GetSelfType == typeof(MovementComponent))
                    {
                        spriteLocation = (component as MovementComponent).location;
                    }
                }

                foreach (BaseComponent component in entity.components)
                {
                    if (component.GetSelfType == typeOfComponent)
                    {
                        //gameWorld.spriteBatch.Draw((component as SpriteComponent).spriteData);
                        // Объекты не должны отрисовываться, если они за кадром 
                        /*if (new Rectangle(Convert.ToInt32(obj.location.X),
                                          Convert.ToInt32(obj.location.Y),
                                          obj.spriteData.widthOfFrame,
                                          obj.spriteData.heightOfFrame).Intersects(ClientPlayerController.CameraFieldOfView))*/

                        gameWorld.spriteBatch.Draw(
                            (component as SpriteComponent).spriteData.CurrentSpriteAtlas,
                            spriteLocation + (component as SpriteComponent).spriteOffset,
                            (component as SpriteComponent).spriteData.sourceRectangleOfFrame,
                            Color.White,
                            (component as SpriteComponent).spriteAngle,
                            new Vector2((component as SpriteComponent).spriteData.widthOfFrame / 2, (component as SpriteComponent).spriteData.heightOfFrame / 2),
                            (component as SpriteComponent).spriteScale,
                            SpriteEffects.None,
                            (component as SpriteComponent).spriteLayer);

                    }
                }
            }
        }

        public SpriteSystem(GameWorld _gameWorld, Type _typeOfComponent) : base(_gameWorld, _typeOfComponent)
        {

        }
    }
}
