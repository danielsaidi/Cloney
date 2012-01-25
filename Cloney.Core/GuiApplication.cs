using System.Collections.Generic;
using System.Linq;
using NExtra.Diagnostics;

namespace Cloney.Core
{

    public class GuiApplication : IProgram
    {
        public GuiApplication(IProcess process)
        {
            Process = process;
        }


        public IProcess Process { get; set; }


        public bool Start(IEnumerable<string> args)
        {
            Process.Start("Cloney.Wizard.exe");
            return true;
        }
    }
}
