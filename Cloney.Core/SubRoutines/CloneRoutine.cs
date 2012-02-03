using System;
using System.Collections.Generic;
using System.Threading;
using Cloney.Core.SolutionCloners;
using NExtra;
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
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly ISolutionCloner solutionCloner;
        private readonly bool threaded;
        private bool cloningInProgress;


        public CloneRoutine()
            :this(Instances.Console, Instances.Translator, Instances.SolutionCloner, true)
        {
        }

        public CloneRoutine(IConsole console, ITranslator translator, ISolutionCloner solutionCloner, bool threaded)
        {
            this.console = console;
            this.translator = translator;
            this.solutionCloner = solutionCloner;
            this.threaded = threaded;
        }



        public void Run(IDictionary<string, string> args)
        {
            if (!args.ContainsKey("clone") || args["clone"] != "true")
                return;

            var sourcePath = GetFolderArg(args, "source");
            if (!ValidateFolderArg(sourcePath, "MissingSourcePathArgumentErrorMessage"))
                return;

            var targetPath = GetFolderArg(args, "target");
            if (!ValidateFolderArg(targetPath, "MissingTargetPathArgumentErrorMessage"))
                return;

            solutionCloner.CloningBegun += solutionCloner_CloningBegun;
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;
            solutionCloner.CurrentPathChanged += solutionCloner_CurrentPathChanged;

            if (threaded)
            {
                new Thread(() => solutionCloner.CloneSolution(sourcePath, targetPath)).Start();
            }
            else
            {
                solutionCloner.CloneSolution(sourcePath, targetPath);
            }

            while (cloningInProgress)
                continue;
        }



        private static string GetFolderArg(IDictionary<string, string> args, string key)
        {
            return (!args.ContainsKey(key) || args[key] == "true")
                       ? string.Empty
                       : args[key];
        }

        private bool ValidateFolderArg(string sourcePath, string errorMessage)
        {
            if (sourcePath == string.Empty)
            {
                console.WriteLine(translator.Translate(errorMessage));
                return false;
            }
            return true;
        }


        void solutionCloner_CloningBegun(object sender, EventArgs e)
        {
            cloningInProgress = true;
        }

        private void solutionCloner_CloningEnded(object sender, EventArgs e)
        {
            cloningInProgress = false;
        }

        void solutionCloner_CurrentPathChanged(object sender, EventArgs e)
        {
            console.WriteLine(solutionCloner.CurrentPath);
        }
    }
}
