using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveWriteLines
{
    public interface IUserInteractionWriter { void Write(string message); }

    public class ConsoleWriter : IUserInteractionWriter
    {
        public void Write(string message) => Console.WriteLine(message);
    }
}
