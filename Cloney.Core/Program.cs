using System;
using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NExtra;
using NExtra.Localization;

namespace Cloney.Core
{
    /// <summary>
    /// This class represents the main Cloney application.
    /// It will trigger all available sub routines with a
    /// parsed collection of input arguments.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class Program : IProgram
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly ICommandLineArgumentParser argumentParser;
        private readonly ISubRoutineLocator subRoutineLocator;


        public Program()
            : this(Default.Console, Default.Translator, Default.CommandLineArgumentParser, Default.SubRoutineLocator)
        {
        }

        public Program(IConsole console, ITranslator translator, ICommandLineArgumentParser argumentParser, ISubRoutineLocator subRoutineLocator)
        {
            this.console = console;
            this.translator = translator;
            this.argumentParser = argumentParser;
            this.subRoutineLocator = subRoutineLocator;
        }



        public void Start(IEnumerable<string> args)
        {
            try
            {
                var arguments = argumentParser.ParseCommandLineArguments(args);
                var routines = subRoutineLocator.FindAll();

                var result = false;
                foreach (var routine in routines)
                    result = result || routine.Run(arguments);

                if (!result)
                    console.WriteLine(translator.Translate("NoRoutineMatchMessage"));
            }
            catch (Exception e)
            {
                console.WriteLine(translator.Translate("StartErrorMessage"));
                console.WriteLine(e.Message);
            }
        }
    }
}
