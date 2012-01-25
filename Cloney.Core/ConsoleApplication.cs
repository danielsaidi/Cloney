using System.Collections.Generic;
using System.Linq;

namespace Cloney.Core
{
    public class ConsoleApplication : IProgram
    {
        public bool Start(IEnumerable<string> args)
        {
            return args.Count() > 0;
        }
    }
}
