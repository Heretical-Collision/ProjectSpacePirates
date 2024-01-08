using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.ECS.Control;

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

        protected override void ComponentTick(Entity entity)
        {
            base.ComponentTick(entity);
            GetCurrentSprite(ComponentOfEntity(entity) as SpriteComponent).Update();
        }

        static public SpriteData GetCurrentSprite(SpriteComponent spriteComponent)
        {   
            if (spriteComponent.spriteData[spriteComponent.currentSpriteName] is not null) return spriteComponent.spriteData[spriteComponent.currentSpriteName];
            else return spriteComponent.spriteData.First().Value;
        }

        static public void SetCurrentSpriteName(SpriteComponent spriteComponent, string newSpriteName)
        {
            if (!spriteComponent.spriteData.Keys.Contains(newSpriteName)) return; //Если название отсутствует в списке анимаций, то воизбежание проблем ничего не меняется

            spriteComponent.spriteData[spriteComponent.currentSpriteName].ResetAnimation();

            spriteComponent.spriteData[newSpriteName].SwitchAnimationPause(false);
            spriteComponent.currentSpriteName = newSpriteName;
        }

        public void Draw()
        {
            foreach (Entity entity in gameWorld.entities)
            {   
                bool doHaveMovementComponent = false;
                Vector2 spriteLocation = new Vector2(0, 0);
                foreach (BaseComponent component in entity.components)
                {
                    if (component is not null) if (component.GetSelfType == typeof(MovementComponent))
                    {
                        spriteLocation = (component as MovementComponent).location;
                        doHaveMovementComponent = true;
                        break;
                    }
                }
                if (doHaveMovementComponent) //объекты не должны рисоваться в игровом мире, если они не имеют присутствия в игровом мире (отсутствуют координаты)
                    foreach (BaseComponent component in entity.components)
                    {
                        if (component is not null) if (component.GetSelfType == typeOfComponent)
                        {
                                // Объекты не должны отрисовываться, если они за кадром 
                                if (new Rectangle(Convert.ToInt32(gameWorld.ClientPlayerController.cameraLocation.X),
                                                  Convert.ToInt32(gameWorld.ClientPlayerController.cameraLocation.Y),
                                                  GetCurrentSprite(component as SpriteComponent).widthOfFrame,
                                                  GetCurrentSprite(component as SpriteComponent).heightOfFrame).Intersects(gameWorld.ClientPlayerController.CameraFieldOfView))
                                {
                                    gameWorld.spriteBatch.Draw(
                                        GetCurrentSprite(component as SpriteComponent).spriteAtlas,
                                        spriteLocation + (component as SpriteComponent).spriteOffset,
                                        GetCurrentSprite(component as SpriteComponent).sourceRectangleOfFrame,
                                        Color.White,
                                        (component as SpriteComponent).spriteAngle,
                                        new Vector2(GetCurrentSprite(component as SpriteComponent).widthOfFrame / 2, GetCurrentSprite(component as SpriteComponent).heightOfFrame / 2),
                                        (component as SpriteComponent).spriteScale,
                                        SpriteEffects.None,
                                        (component as SpriteComponent).spriteLayer);
                                    break;
                                }
                        }
                    }
                doHaveMovementComponent = false;
            }
        }

        public SpriteSystem(GameWorld _gameWorld, Type _typeOfComponent) : base(_gameWorld, _typeOfComponent)
        {

        }
    }
}
