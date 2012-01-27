using System.Collections.Generic;
using NExtra.Diagnostics;

namespace Cloney.Core.Gui
{
    /// <summary>
    /// This class is responsible for starting the Cloney
    /// GUI application by starting a separate process.
    /// </summary>
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
