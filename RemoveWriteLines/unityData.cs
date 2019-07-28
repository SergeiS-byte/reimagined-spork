using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace RemoveWriteLines
{
    public class unityData
    {
        public static UnityContainer container;

        public void SetContainer()
        {
            container = new UnityContainer();
            container.RegisterType<IUserInteractionReader, ConsoleReader>();
            container.RegisterType<IUserInteractionWriter, ConsoleWriter>();
            container.RegisterType<ILogicElement, ReplaceConsole>("Проверка замены Console");
        }
    }
}
