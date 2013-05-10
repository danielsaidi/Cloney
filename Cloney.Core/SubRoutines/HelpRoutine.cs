using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine displays help information. It is
    /// triggered by the "cloney --help" command.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class HelpRoutine : SubRoutineBase, ISubRoutine
    {
        public HelpRoutine()
            : this(Default.CommandLineArgumentParser, Default.Console, Default.Translator)
        {
        }

        public HelpRoutine(ICommandLineArgumentParser argumentParser, IConsole console, ITranslator translator)
            : base(argumentParser, console, translator)
        {
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(ArgumentParser.ParseCommandLineArguments(args));
        }

        private bool Run(CommandLineArguments args)
        {
            if (!args.HasSingleArgument("help", "true"))
                return false;

            var message = Translator.Translate("GeneralHelpMessage");
            Console.WriteLine(message);
            return true;
        }
    }
}
