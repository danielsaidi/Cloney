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
        public Program()
            : this(new ConsoleFacade(), new LanguageProvider(), new CommandLineArgumentParser(), new LocalSubRoutineLocator())
        {
        }

        public Program(IConsole console, ITranslator translator, ICommandLineArgumentParser argumentParser, ISubRoutineLocator subRoutineLocator)
        {
            Console = console;
            Translator = translator;
            ArgumentParser = argumentParser;
            SubRoutineLocator = subRoutineLocator;
        }


        private ICommandLineArgumentParser ArgumentParser { get; set; }

        private IConsole Console { get; set; }

        private ITranslator Translator { get; set; }

        private ISubRoutineLocator SubRoutineLocator { get; set; }


        public void Start(IEnumerable<string> args)
        {
            try
            {
                var arguments = ArgumentParser.ParseCommandLineArguments(args);
                var routines = SubRoutineLocator.FindAll();

                foreach (var routine in routines)
                    routine.Run(arguments);
            }
            catch (Exception e)
            {
                Console.WriteLine(Translator.Translate("StartErrorMessage"));
                Console.WriteLine(e.Message);
            }
        }
    }
}
