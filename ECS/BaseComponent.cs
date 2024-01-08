using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS
{
    public abstract class BaseComponent
    {
        public int componentTypeID = 0;
        public Type GetSelfType { get { return GetType(); } }
        public Entity entityOfComponent = null;
        public readonly bool isReplicateble = false; //Данная переменная должна отвечать на вопрос, передавать ли данный компонент на клиент.
        public BaseSystem systemOfComponent;        //Пример: клиенту нужно знать spritecomponent, т.к. он должен отрисовывать картинки. Но в то же время
                                                   //клиенту не нужно знать, к примеру, какое у этого объекта направление управления (входные данные с контроллера на сервере)
    
    }
}
