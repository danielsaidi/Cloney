using System;
using System.Collections.Generic;
using Cloney.Core.Cloners;
using Cloney.Core.Console;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This routine will trigger on this console command:
    /// cloney --clone --source=x --target=y
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class CloneRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly ICommandLineArgumentParser argumentParser;
        private readonly ISolutionCloner solutionCloner;


        public CloneRoutine()
            :this(Default.Console, Default.Translator, Default.CommandLineArgumentParser, Default.SolutionCloner)
        {
        }

        public CloneRoutine(IConsole console, ITranslator translator, ICommandLineArgumentParser argumentParser, ISolutionCloner solutionCloner)
        {
            this.console = console;
            this.translator = translator;
            this.argumentParser = argumentParser;
            this.solutionCloner = solutionCloner;

            solutionCloner.CurrentPathChanged += solutionCloner_CurrentPathChanged;
        }

        
        public bool Run(IEnumerable<string> args)
        {
            return Run(argumentParser.ParseCommandLineArguments(args));
        }

        private bool Run(IDictionary<string, string> args)
        {
            if (!HasArg(args, "clone", "true"))
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
            if (string.IsNullOrEmpty(path.Trim()))
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
