using System.Collections.Generic;
using NExtra;
using NExtra.Localization;

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


        public HelpRoutine()
            :this(Default.Console, Default.Translator)
        {
        }

        public HelpRoutine(IConsole console, ITranslator translator)
        {
            this.console = console;
            this.translator = translator;
        }


        public bool Run(IDictionary<string, string> args)
        {
            if (!ArgsHaveSingleKeyValue(args, "help", "true"))
                return false;
            
            console.WriteLine(translator.Translate("GeneralHelpMessage"));
            return true;
        }
    }
}
