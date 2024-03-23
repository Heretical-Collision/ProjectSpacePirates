using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS.Sprite;
using static ProjectSpaceProject.StaticECSMethods;
using ProjectSpaceProject.ECS;
using System.ComponentModel;


namespace ProjectSpaceProject.ECS.Collision;

[SystemsOrder("Physics")]
public class CollisionSystem : BaseSystem
{
    public override void Update()
    {
        base.Update();
        var sw = new Stopwatch();
        //sw.Start();
        CheckAllIntersections();
        //sw.Stop();
        //Debug.WriteLine(sw.ElapsedMilliseconds + "ms with collisions amount - " + gameWorld.componentsByType[typeof(CollisionComponent)].Item1.Count);
    }

    private void CheckAllIntersections()
    {
        CollisionComponent[] ccListRef = GetAllComponents<CollisionComponent>().ToArray();
        List<LocationComponent> locComponentsTemp = new List<LocationComponent>();
        List<float> locationXlist = new List<float>();
        List<float> locationYlist = new List<float>();
        List<float> rectangleWidthList = new List<float>();
        List<float> rectangleHeightList = new List<float>();
        List<float> circleRadiusList = new List<float>();

        foreach (CollisionComponent cc in ccListRef)
        {
            LocationComponent tlc = GetComponentOfEntity<LocationComponent>(cc.entityOfComponent);
            if (tlc is not null)
            {
                locComponentsTemp.Add(tlc);

                locationXlist.Add(tlc.location.X + cc.collisionOffset.X);
                locationYlist.Add(tlc.location.Y - cc.collisionOffset.Y);
                rectangleWidthList.Add(cc.rectangleCollision.Width);
                rectangleHeightList.Add(cc.rectangleCollision.Height);
                circleRadiusList.Add(cc.circleCollision.radius);
            }
        }
        float[] locationX = locationXlist.ToArray();
        float[] locationY = locationYlist.ToArray();
        float[] rectangleWidth = rectangleWidthList.ToArray();
        float[] rectangleHeight = rectangleHeightList.ToArray();
        float[] circleRadius = circleRadiusList.ToArray();

        for (int i = 0; i < ccListRef.Length; i++)
        {
            if (ccListRef[i].isStatic || ccListRef[i].collisionType == CollisionInteractionType.Block) { continue; } //Если объект статичный, он ни с чем не пересечется сам по себе. Если объект только блокирует, то это событие никогда не будет вызвано, т.к. блокировка не дает пересекаться
            CheckIntersectionsFor(i, ccListRef, locationX, locationY, rectangleWidth, rectangleHeight, circleRadius);
        }
    }

    private void CheckIntersectionsFor(int componentID, CollisionComponent[] ccListRef, float[] locationX, float[] locationY, float[] rectangleWidth, float[] rectangleHeight, float[] circleRadius)
    {
        for (int i = 0; i < ccListRef.Length; i++)
        {
            if (ccListRef[componentID] == ccListRef[i]) continue; 
            if (CheckIntersections(locationX[componentID], locationY[componentID], rectangleWidth[componentID], rectangleHeight[componentID], circleRadius[componentID], locationX[i], locationY[i], rectangleWidth[i], rectangleHeight[i], circleRadius[i]))
                Collide(ccListRef[componentID], ccListRef[i]);
        }
    }

    /*static private bool CheckIntersectionsObj(CollisionComponent cc, CollisionComponent cc2, Vector2 location1, Vector2 location2)
    {
        if (cc.circleCollision.radius != 0 && cc2.circleCollision.radius != 0)
        {
            Circle circle1 = new Circle(new Vector2(location1.X + cc.circleCollision.x, location1.Y + cc.circleCollision.y), cc.circleCollision.radius);
            Circle circle2 = new Circle(new Vector2(location2.X + cc2.circleCollision.x, location2.Y + cc2.circleCollision.y), cc2.circleCollision.radius);
            return circle1.Intersects(circle2);
        }

        Rectangle rectangle1 = new Rectangle((int)location1.X + cc.rectangleCollision.Location.X, (int)location1.Y + cc.rectangleCollision.Location.Y, cc.rectangleCollision.Width, cc.rectangleCollision.Height);
        Rectangle rectangle2 = new Rectangle((int)location2.X + cc2.rectangleCollision.Location.X, (int)location2.Y + cc2.rectangleCollision.Location.Y, cc2.rectangleCollision.Width, cc2.rectangleCollision.Height);

        if (!(rectangle1.Width == 0 && rectangle1.Height == 0) && !(rectangle2.Width == 0 && rectangle2.Height==0))
        {
            return rectangle1.Intersects(rectangle2);
        }

        if (cc.circleCollision.radius != 0 && !(rectangle2.Width == 0 && rectangle2.Height == 0))
        {
            Circle circle1 = new Circle(new Vector2(location1.X + cc.circleCollision.x, location1.Y + cc.circleCollision.y), cc.circleCollision.radius);
            return circle1.IntersectsWithRectangle(rectangle2);
        }

        if (!(rectangle1.Width == 0 && rectangle1.Height == 0) && cc2.circleCollision.radius != 0)
        {
            Circle circle2 = new Circle(new Vector2(location2.X + cc2.circleCollision.x, location2.Y + cc2.circleCollision.y), cc2.circleCollision.radius);
            return circle2.IntersectsWithRectangle(rectangle1);
        }

        return false;
    }*/

    static private bool CheckIntersections(float locationXFirst, float locationYFirst, float rectangleWidthFirst, float rectangleHeightFirst, float circleRadiusFirst, float locationXSecond, float locationYSecond, float rectangleWidthSecond, float rectangleHeightSecond, float circleRadiusSecond)
    {
        if (circleRadiusFirst != 0 && circleRadiusSecond != 0)
        {
            return Circle.IntersectsStatic(locationXFirst, locationYFirst, circleRadiusFirst, locationXSecond, locationYSecond, circleRadiusSecond);
        }

        if (!(rectangleWidthFirst == 0 && rectangleHeightFirst == 0) && !(rectangleWidthSecond == 0 && rectangleHeightSecond == 0))
        {
            return IntersectsRectangle(locationXFirst, locationYFirst, rectangleWidthFirst, rectangleHeightFirst, locationXSecond, locationYSecond, rectangleWidthSecond, rectangleHeightSecond);
        }

        if (circleRadiusFirst != 0 && !(rectangleWidthSecond == 0 && rectangleHeightSecond == 0))
        {
            return Circle.IntersectsWithRectangleStatic(locationXFirst, locationYFirst, circleRadiusFirst, locationXSecond, locationYSecond, rectangleWidthSecond, rectangleHeightSecond);
        }

        if (!(rectangleWidthFirst == 0 && rectangleHeightFirst == 0) && circleRadiusSecond != 0)
        {
            return Circle.IntersectsWithRectangleStatic(locationXSecond, locationYSecond, circleRadiusSecond, locationXFirst, locationYFirst, rectangleWidthFirst, rectangleHeightFirst);
        }

        return false;

        bool IntersectsRectangle(float locationXFirst, float locationYFirst, float rectangleWidthFirst, float rectangleHeightFirst, float locationXSecond, float locationYSecond, float rectangleWidthSecond, float rectangleHeightSecond)
        {

            if (locationXFirst < locationXSecond + rectangleWidthSecond && locationXSecond < locationXFirst + rectangleWidthFirst && locationYFirst < locationYSecond + rectangleHeightSecond)
            {
                return locationYSecond < locationYFirst + rectangleHeightFirst;
            }

            return false;
        }
    }

    static private void Collide(CollisionComponent collision1, CollisionComponent collision2)
    {
        SpawnEntity(new List<BaseComponent>{new CollisionDataComponent(collision1.entityOfComponent, collision2.entityOfComponent)});
    }

    static public bool IntersectWithAnything(CollisionComponent collisionComponent, LocationComponent locationComponent)
    {

        if (collisionComponent.collisionType == CollisionInteractionType.Overlap) return false; //Если объект не может быть заблокирован, то не нужно проверять его на пересечение здесь

        float x1;
        float y1;
        float w1 = 0;
        float h1 = 0;
        float r1 = 0;
        float x2;
        float y2;
        float w2 = 0;
        float h2 = 0;
        float r2 = 0;

        List<LocationComponent> locationComponentsList = new List<LocationComponent>();
        CollisionComponent[] collisionComponents = GetAllComponents<CollisionComponent>().ToArray();

        for (int i = 0; i < collisionComponents.Length; i++) locationComponentsList.Add(GetComponentOfEntity<LocationComponent>(collisionComponents[i].entityOfComponent));

        LocationComponent[] locationComponents = locationComponentsList.ToArray();

        x1 = locationComponent.location.X + collisionComponent.collisionOffset.X;
        y1 = locationComponent.location.Y - collisionComponent.collisionOffset.Y;
        w1 = collisionComponent.rectangleCollision.Width;
        h1 = collisionComponent.rectangleCollision.Height;
        r1 = collisionComponent.circleCollision.radius;


        for (int i = 0; i < collisionComponents.Length; i++) 
        {
            if (collisionComponents[i] == collisionComponent || collisionComponents[i].collisionType == CollisionInteractionType.Overlap) continue;

            x2 = locationComponents[i].location.X + collisionComponents[i].collisionOffset.X;
            y2 = locationComponents[i].location.Y - collisionComponents[i].collisionOffset.Y;
            w2 = collisionComponents[i].rectangleCollision.Width;
            h2 = collisionComponents[i].rectangleCollision.Height;
            r2 = collisionComponents[i].circleCollision.radius;

            if (CheckIntersections(x1, y1, w1, h1, r1, x2, y2, w2, h2, r2))
            {
                if (collisionComponents[i].collisionType == CollisionInteractionType.Block)
                {
                    return true;
                }
                else
                {
                    Collide(collisionComponent, collisionComponents[i]);
                }
            }
        }

        return false;
    }
}

public enum CollisionInteractionType
{
    Block,
    Overlap,
    BlockAndOverlap,
    SoftBlock
}
