using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on this console command:
    /// cloney --help
    /// When triggered, it displays Cloney help info text.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class HelpRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly ICommandLineArgumentParser<IDictionary<string, string>> argumentParser;


        public HelpRoutine()
            :this(Default.Console, Default.Translator)
        {
        }

        public HelpRoutine(IConsole console, ITranslator translator)
        {
            this.console = console;
            this.translator = translator;

            argumentParser = Default.DictionaryCommandLineArgumentParser;
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
