using System.Collections.Generic;
using System.Linq;
using NExtra;

namespace Cloney.Core
{
    public class ConsoleApplication : IProgram
    {
        public ConsoleApplication(IConsole console)
        {
            Console = console;
        }


        private IConsole Console { get; set; }


        public bool Start(IEnumerable<string> args)
        {
            return args.Count() > 0;
        }
    }
}
