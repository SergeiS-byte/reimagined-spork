using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWriteLines
{
    public interface IUserInteractionReader { string Read(); }

    public class ConsoleReader : IUserInteractionReader
    {
        public string Read() => Console.ReadLine();
    }
}
