using System;
using Cloney.Core.Abstractions;

namespace Cloney.Core
{
    public class ConsoleFacade : IConsole
    {
        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
