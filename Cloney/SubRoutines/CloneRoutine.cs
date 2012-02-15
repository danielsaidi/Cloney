using System;
using System.Collections.Generic;
using Cloney.Cloners;
using NExtra;
using NExtra.Extensions;
using NExtra.Localization;

namespace Cloney.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger on the --clone arg,
    /// using --source=x --target=y as input parameters.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
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


        public bool Run(IDictionary<string, string> args)
        {
            if (!args.ContainsKey("clone") || args["clone"] != "true")
                return false;

            var sourcePath = GetPath(args, "source");
            var targetPath = GetPath(args, "target");

            solutionCloner.CloneSolution(sourcePath, targetPath);
            return true;
        }


        private static string GetArg(IDictionary<string, string> args, string key)
        {
            return (!args.ContainsKey(key) || args[key] == "true")
                       ? string.Empty
                       : args[key];
        }

        private string GetPath(IDictionary<string, string> args, string type)
        {
            var path = GetArg(args, type);
            if (path.Trim().IsNullOrEmpty())
                path = GetPathFromConsole(type);
            return path;
        }

        private string GetPathFromConsole(string type)
        {
            var message = string.Format(translator.Translate("EnterFolderPath"), type);
            console.Write(message);
            return console.ReadLine();
        }


        void solutionCloner_CurrentPathChanged(object sender, EventArgs e)
        {
            console.WriteLine(solutionCloner.CurrentPath);
        }
    }
}
