﻿using System;
using System.Collections.Generic;
using Cloney.Core.Cloners;
using Cloney.Core.Console;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on this console command:
    /// cloney --clone [--source=x] [--target=y]
    /// When triggered, it will clone the solution x to y.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class CloneRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly ISolutionCloner solutionCloner;


        public CloneRoutine()
            : this(Default.SolutionCloner, Default.CommandLineArgumentParser, Default.Console, Default.Translator)
        {
        }

        public CloneRoutine(ISolutionCloner solutionCloner, ICommandLineArgumentParser argumentParser, IConsole console, ITranslator translator)
            : base(argumentParser, console, translator)
        {
            this.solutionCloner = solutionCloner;

            solutionCloner.CurrentPathChanged += solutionCloner_CurrentPathChanged;
        }

        
        public bool Run(IEnumerable<string> args)
        {
            return Run(ArgumentParser.ParseCommandLineArguments(args));
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


        private string GetPath(IDictionary<string, string> args, string pathType)
        {
            var path = GetPathArg(args, pathType);
            return string.IsNullOrWhiteSpace(path) ? GetPathFromConsole(pathType) : path;
        }

        private static string GetPathArg(IDictionary<string, string> args, string key)
        {
            return (!args.ContainsKey(key) || args[key] == "true")
                       ? string.Empty
                       : args[key];
        }

        private string GetPathFromConsole(string type)
        {
            var message = string.Format(Translator.Translate("EnterFolderPath"), type);
            Console.Write(message);
            var result = Console.ReadLine();

            return result;
        }


        private void solutionCloner_CurrentPathChanged(object sender, EventArgs e)
        {
            Console.WriteLine(solutionCloner.CurrentPath);
        }
    }
}
