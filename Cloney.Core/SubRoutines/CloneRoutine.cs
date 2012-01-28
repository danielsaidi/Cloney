using System;
using System.Collections.Generic;
using System.Threading;
using Cloney.Core.SolutionCloners;
using NExtra;
using NExtra.Diagnostics;
using NExtra.Localization;

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
        private ITranslator translator;
        private ISolutionCloner solutionCloner;


        public CloneRoutine()
            :this(new ConsoleFacade(), new LanguageProvider(), new SolutionCloner())
        {
        }

        public CloneRoutine(IConsole console, ITranslator translator, ISolutionCloner solutionCloner)
        {
            this.console = console;
            this.translator = translator;
            this.solutionCloner = solutionCloner;
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

        private void RunCloningOperation(string sourceFolder, string targetFolder)
        {
            
        }
    }
}
