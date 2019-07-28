using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace RemoveWriteLines
{
    public interface ILogicElement { void WriteD(string Input); string ReadD(); }

    public class ReplaceConsole : ILogicElement
    {
        IUserInteractionReader _reader;
        IUserInteractionWriter _writer;

        public ReplaceConsole(IUserInteractionReader reader, IUserInteractionWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public string ReadD()
        {
            _writer.Write("Введите ваши данные: ");
            //var data = _reader.Read();
            return _reader.Read();
        }

        public void WriteD(string Input)
        {
            //_writer.Write("Здесь будут выведены ваши данные:");
            _writer.Write($"{Input}");
        }
    }

    public class Bootstrapper
    {
        IUnityContainer _container;
        public Bootstrapper(IUnityContainer container)
        {
            _container = container;
        }

        public void WriteAndGo(string InputData)
        {
            foreach (var logic in _container.ResolveAll<ILogicElement>())
                logic.WriteD(InputData);
        }

        public void ReadAndGo()
        {
            foreach (var logic in _container.ResolveAll<ILogicElement>())
                logic.ReadD();          
        }
    }
}
