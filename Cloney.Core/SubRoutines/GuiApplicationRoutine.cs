using System.Collections.Generic;
using NExtra.Diagnostics;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger if no arguments
    /// are provided. It will start the Cloney wizard
    /// and display some start instructions.
    /// </summary>
    public class GuiApplicationRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IProcess process;


        public GuiApplicationRoutine()
            :this(Default.Process)
        {
        }

        public GuiApplicationRoutine(IProcess process)
        {
            this.process = process;
        }


        public bool Run(IDictionary<string, string> args)
        {
            if (args.Count != 0)
                return false;

            process.Start("Cloney.Wizard.exe");
            return true;
        }
    }
}
