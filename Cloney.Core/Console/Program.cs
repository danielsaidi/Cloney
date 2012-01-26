using System.Collections.Generic;
using System.Linq;
using NExtra;

namespace Cloney.Core.Console
{
    public class Program : IProgram
    {
        public Program(IConsole console, ICommandLineArgumentParser argumentParser)
        {
            Console = console;
            ArgumentParser = argumentParser;
        }


        public ICommandLineArgumentParser ArgumentParser { get; set; }

        private IConsole Console { get; set; }


        public bool Start(IEnumerable<string> args)
        {
            var arguments = ArgumentParser.ParseCommandLineArguments(args);

            return args.Count() > 0;
        }
    }
}
