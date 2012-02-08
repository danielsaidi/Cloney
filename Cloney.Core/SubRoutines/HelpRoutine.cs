using System.Collections.Generic;
using NExtra;
using NExtra.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This routine will run when the argument collection
    /// contains a help key with the value "true". It will
    /// then print general Cloney help information.
    /// </summary>
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
