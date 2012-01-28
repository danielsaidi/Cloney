using System;
using System.Collections.Generic;
using System.Threading;
using NExtra;
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
        private IConsole console;


        public CloneRoutine()
            :this(new ConsoleFacade())
        {
        }

        public CloneRoutine(IConsole console)
        {
            this.console = console;
        }


        public void Run(IDictionary<string, string> args)
        {
            if (args.ContainsKey("clone") && args["clone"] == "true")
                Thread.Sleep(2000);
            //if (!ArgsHaveKeyValue("clone", "true"))
              //  return Finish();

            //if (args.Count == 0)
              //  Process.Start("Cloney.Wizard.exe");

            Finish();
        }
    }
}
