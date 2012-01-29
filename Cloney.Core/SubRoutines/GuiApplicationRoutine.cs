using System.Collections.Generic;
using NExtra.Diagnostics;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger on the --help
    /// argument and print information about how to
    /// use Cloney.
    /// </summary>
    public class GuiApplicationRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IProcess process;


        public GuiApplicationRoutine()
            :this(Instances.Process)
        {
        }

        public GuiApplicationRoutine(IProcess process)
        {
            this.process = process;
        }


        public void Run(IDictionary<string, string> args)
        {
            if (args.Count == 0)
                process.Start("Cloney.Wizard.exe");

            Finish();
        }
    }
}
