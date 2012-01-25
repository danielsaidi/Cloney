using System.Collections.Generic;
using System.Linq;

namespace Cloney.Core
{
    public class GuiApplication : IProgram
    {
        public bool Start(IEnumerable<string> args)
        {
            return args.Count() == 0;
        }
    }
}
