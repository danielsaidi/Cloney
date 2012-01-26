using System.Collections.Generic;
using NExtra.Diagnostics;

namespace Cloney.Core.Gui
{
    public class Program : IProgram
    {
        public Program(IProcess process)
        {
            Process = process;
        }


        private IProcess Process { get; set; }


        public bool Start(IEnumerable<string> args)
        {
            Process.Start("Cloney.Wizard.exe");
            return true;
        }
    }
}
