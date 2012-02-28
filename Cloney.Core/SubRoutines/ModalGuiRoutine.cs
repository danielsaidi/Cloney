using System.Collections.Generic;
using System.Linq;
using Cloney.Core.Console;
using Cloney.Core.Diagnostics;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on this console command:
    /// cloney --modal
    /// When triggered, it starts the Cloney GUI in modal
    /// mode, which only displays a modal window.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class ModalGuiRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IProcess process;
        private readonly IArgumentParser<IDictionary<string, string>> argumentParser;


        public ModalGuiRoutine()
            :this(Default.Process)
        {
        }

        public ModalGuiRoutine(IProcess process)
        {
            this.process = process;

            argumentParser = Default.DictionaryArgumentParser;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(argumentParser.ParseArguments(args));
        }

        private bool Run(IDictionary<string, string> args)
        {
            if (!HasSingleArg(args, "modal", "true"))
                return false;

            process.Start("Cloney.Wizard.exe", "--modal");
            return true;
        }
    }
}
