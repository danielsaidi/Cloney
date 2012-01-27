using System.Collections.Generic;
using System.Linq;
using Cloney.Core.SubRoutines;
using NExtra;

namespace Cloney.Core.Console
{
    /// <summary>
    /// This class is the main Cloney console application.
    /// It delegates its input arguments to all available
    /// sub routines, which then trigger on the arguments
    /// and perform something...anything.
    /// </summary>
    public class Program : IProgram
    {
        public Program(IConsole console, ICommandLineArgumentParser argumentParser, ISubRoutineLocator subRoutineLocator)
        {
            Console = console;
            ArgumentParser = argumentParser;
            SubRoutineLocator = subRoutineLocator;
        }


        public ICommandLineArgumentParser ArgumentParser { get; set; }

        private IConsole Console { get; set; }

        public ISubRoutineLocator SubRoutineLocator { get; set; }


        public bool Start(IEnumerable<string> args)
        {
            var arguments = ArgumentParser.ParseCommandLineArguments(args);
            var routines = SubRoutineLocator.FindAll();

            foreach(var routine in routines)
                routine.Run(arguments);

            return args.Count() > 0;
        }
    }
}
