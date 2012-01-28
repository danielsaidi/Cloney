using System.Collections.Generic;
using NExtra.Diagnostics;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger on the --help
    /// argument and print information about how to
    /// use Cloney.
    /// </summary>
    public class CloneRoutine : SubRoutineBase, ISubRoutine
    {
        public CloneRoutine()
            :this(new ProcessFacade())
        {
        }

        public CloneRoutine(IProcess process)
        {
            Process = process;
        }


        private IProcess Process { get; set; }


        public void Run(IDictionary<string, string> args)
        {
            //if (args.Count == 0)
              //  Process.Start("Cloney.Wizard.exe");

            Finish();
        }
    }
}
