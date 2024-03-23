using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS.Collision;
using ProjectSpaceProject.ECS.Control;
using ProjectSpaceProject.ECS.Movement;
using ProjectSpaceProject.ECS.Sprite;
using ProjectSpaceProject.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using static ProjectSpaceProject.StaticECSMethods;
using ProjectSpaceProject.EventManage;
using System.Diagnostics;

namespace ProjectSpaceProject
{
    public class ServerSystemManager
    {
        public GameWorld gameWorld;
        public List<Entity> entitiesToDestroy = new List<Entity>();
        public List<BaseComponent> componentsToDestroy = new List<BaseComponent>();

        public void Update(GameTime gameTime)
        {
            foreach (BaseSystem sys in gameWorld.systems)
            {
                sys.Update();
            }
            DestroyEntities(entitiesToDestroy);
            DestroyComponents(componentsToDestroy);
        }

        private void ServerInitialize()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> types = new List<Type>();
            List<Type> tempComps = assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseComponent) && typeof(BaseComponent).IsAssignableFrom(t)).ToList();
            List<BaseSystem> tempSys = new List<BaseSystem>();

            foreach (Type t in assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseSystem) && typeof(BaseSystem).IsAssignableFrom(t)).ToList()) 
            {
                tempSys.Add((BaseSystem)t.GetConstructors()[0].Invoke(new object[0]{ }));         //Берёт тип системы, и вызывает его конструктор.
            }
            List<BaseSystem> tList = (from t in tempSys 
                                     where t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) is not null 
                                     orderby (t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) as SystemsOrderAttribute).orderType, 
                                             (t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) as SystemsOrderAttribute).priority
                                     select t)
                                     .Concat(from t in tempSys
                                                       where t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) is null
                                                       select t).ToList();
            //Манипуляции при помощи LINQ нужны для создания порядка работы систем на основании данных аттрибута SystemsOrderAttribute
            gameWorld.systems = tList.ToArray();
            for (int i = 0; i < tempComps.Count - 1; i++)
            {
                gameWorld.componentsByType.Add(tempComps[i], (new List<BaseComponent>(), i));
                StaticECSMethods.componentIndices.Add(tempComps[i], i);
            }

            gameWorld.eventManager = new EventManager();

            foreach (BaseSystem s in tempSys) //Инициализирует системы
            {
                s.gameWorld = gameWorld;
                if (s is SpriteSystem) gameWorld.spriteSystem = (SpriteSystem)s;
                s.Initialize();
            }
        }

        public ServerSystemManager(GameWorld _gameWorld)
        {
            gameWorld = _gameWorld;
            ServerInitialize();
        }
    }
}
