using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ProjectSpaceProject.ECS;
using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.ECS.Sprite;
using ProjectSpaceProject.ECS.Control;
using ProjectSpaceProject.ECS.Collision;
using System.ComponentModel;
using System.Xml.Linq;

namespace ProjectSpaceProject
{
    public class GameWorld
    {
        public GameI gameInstance;
        public SpriteBatch spriteBatch;                                             
        public float screenScale = 1;
        public Matrix matrix;
        public PlayerController ClientPlayerController { get { return (gameInstance.controllers.Count > 0) ? (gameInstance.controllers[0] as PlayerController) : null; } }
        public MapGenerator mapGenerator;
        public List<Entity> entities = new List<Entity>();
        public Dictionary<Type, List<Entity>> entitiesBySystems = new Dictionary<Type, List<Entity>>();
        public BaseSystem[] systems;
        public Type[] componentsTypesID;


        public void SpawnEntity(List<BaseComponent> components)
        {
            Entity tempEntity = new Entity(components, componentsTypesID);
            foreach (BaseSystem s in systems)
            {
                foreach (BaseComponent c in components)
                {
                    if (s.typeOfComponent == c.GetType()) entitiesBySystems[s.GetType()].Add(tempEntity);
                }
            }
            entities.Add(tempEntity);
        }

        public void AddPlayer(Vector2 location)
        {
            ControlComponent tempRefControlComponent = new ControlComponent();

            SpawnEntity(new List<BaseComponent>() {
                            new MovementComponent(new Vector2(50, 50), new Vector2(0, 0), 1.5f),
                            new SpriteComponent(new Dictionary<string, SpriteData>(){
                                               { "WalkRight", new SpriteData(gameInstance.spriteList["playerCharacterRight"], 1, 6, 6, 0)},
                                               { "WalkDown", new SpriteData(gameInstance.spriteList["playerCharacterDown"], 1, 6, 6, 0)},
                                               { "WalkLeft", new SpriteData(gameInstance.spriteList["playerCharacterLeft"], 1, 6, 6, 0)},
                                               { "WalkUp", new SpriteData(gameInstance.spriteList["playerCharacterUp"], 1, 6, 6, 0)}},
                                new Vector2(0, 0),
                                1f),
                            new CollisionComponent(new Circle(16)),
                            tempRefControlComponent});
            ControlSystem.ConnectControllerToComponent(tempRefControlComponent, gameInstance.controllers[0]);
        }

        public void Update(GameTime gameTime)
        {
            foreach(BaseSystem sys in systems)
            {
                sys.Update();
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (gameInstance.controllers.Count > 0)
            {
                spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack,
                    samplerState: SamplerState.PointClamp,
                    transformMatrix: Matrix.CreateTranslation(-ClientPlayerController.cameraLocation.X, -ClientPlayerController.cameraLocation.Y, 0f) *
                        Matrix.CreateScale(ClientPlayerController.cameraZoom * screenScale, ClientPlayerController.cameraZoom * screenScale, 1f) *
                        Matrix.CreateTranslation(gameInstance._graphics.PreferredBackBufferWidth / 2, gameInstance._graphics.PreferredBackBufferHeight / 2, 0));
                (systems[0] as SpriteSystem).Draw();
                spriteBatch.End();
            }
        }

        public GameWorld(GameI _gameInstance, SpriteBatch _spriteBatch) 
        {
            gameInstance = _gameInstance;
            spriteBatch = _spriteBatch;
            Run();
        }

        public void Run()
        {
            gameInstance.controllers.Add(new PlayerController(gameInstance, this));
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> types = new List<Type>();

            List<Type> tempComps = new List<Type>();
            foreach (Type t in assembly.GetTypes())
            {  
                if (t.IsClass && t.Namespace is not null && t.Namespace.Contains("ECS"))    //Проверка должна идти только в папке ECS
                {
                    if (t.Name.Contains("Component") && t != typeof(BaseComponent))         //Добавляет все классы, имеющие в названии слово "Компонент" в список компонентов для оптимизации поиска. Абстрактный класс исключен.
                    {
                        tempComps.Add(t);
                    }
                }
            }
            componentsTypesID = tempComps.ToArray();
            List<BaseSystem> sys = new List<BaseSystem>();

            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsClass && t.Namespace is not null && t.Namespace.Contains("ECS"))
                {
                    if (t.Name.Contains("System") && t != typeof(BaseSystem))               //Добавляет все классы, имеющие в названии слово "Система" для автоматической загрузки систем. Сделано для удобства. Абстрактный класс исключен.
                    {
                        sys.Add(
                            t.GetConstructors()[0].Invoke(new object[2]         //Берёт пододящий найденный тип, и вызывает его конструктор. В случае BaseSystem в конструкторе два параметра
                            { this,                                            //GameWorld в конструктор
                              tempComps.Find(                                 //И тип компонента, соответствующего системе, для постоянного вызова метода Update(). Он должен найтись исходя из загруженных классов.
                                    tc => tc.Name.Contains(                  //Лямбда-выражение в качестве предиката для Find(), которое должно найти подходящий компонент
                                        t.Name.Remove(t.Name.Length - 7)))}) as BaseSystem);      //Путём удаления из полного названия системы слова "System" в конце, и поиска компонента
                                                                                                 //С совпадающим названием   
                        entitiesBySystems.Add(sys.Last().GetType(), new List<Entity>());
                    }
                }
            }
            systems = sys.ToArray();


            AddPlayer(new Vector2(0, 0));
            mapGenerator = new MapGenerator(this);
        }

        public void AdaptiveScreenScale()
        {
            screenScale = gameInstance._graphics.PreferredBackBufferWidth / 1280 * 1;
        }
    }
}
    