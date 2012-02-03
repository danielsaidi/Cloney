using System;
using System.Collections.Generic;
using Cloney.Core.Cloners;
using NExtra;
using NExtra.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger on the --clone
    /// argument, and clone --source=x to --target=y.
    /// </summary>
    public class CloneRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly ISolutionCloner solutionCloner;


        public CloneRoutine()
            :this(Default.Console, Default.Translator, Default.SolutionCloner)
        {
        }

        public CloneRoutine(IConsole console, ITranslator translator, ISolutionCloner solutionCloner)
        {
            this.console = console;
            this.translator = translator;
            this.solutionCloner = solutionCloner;

            solutionCloner.CurrentPathChanged += solutionCloner_CurrentPathChanged;
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

            solutionCloner.CloneSolution(sourcePath, targetPath);
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


        void solutionCloner_CurrentPathChanged(object sender, EventArgs e)
        {
            console.WriteLine(solutionCloner.CurrentPath);
        }
    }
}
