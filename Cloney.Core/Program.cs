using System;
using System.Collections.Generic;
using NExtra;
using NExtra.Localization;

namespace Cloney.Core
{
    /// <summary>
    /// This class represents the main Cloney application.
    /// It will either trigger the console or the GUI app,
    /// according to the input argument it receives.
    /// </summary>
    public class Program : IProgram
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly ICommandLineArgumentParser argumentParser;
        private readonly ISubRoutineLocator subRoutineLocator;


        public Program()
            : this(new ConsoleFacade(), new LanguageProvider(), new CommandLineArgumentParser(), new LocalSubRoutineLocator())
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
