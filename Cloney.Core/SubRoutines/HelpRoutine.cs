﻿using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on the --help arg. It
    /// prints general help information to the console.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class HelpRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly ICommandLineArgumentParser argumentParser;


        public HelpRoutine()
            :this(Default.Console, Default.Translator, Default.CommandLineArgumentParser)
        {
        }

        public HelpRoutine(IConsole console, ITranslator translator, ICommandLineArgumentParser argumentParser)
        {
            this.console = console;
            this.translator = translator;
            this.argumentParser = argumentParser;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(argumentParser.ParseCommandLineArguments(args));
        }

        private bool Run(IDictionary<string, string> args)
        {
            if (!HasSingleArg(args, "help", "true"))
                return false;
            
            console.WriteLine(translator.Translate("GeneralHelpMessage"));
            return true;
        }
    }
}
