using System.Collections.Generic;
using System.Linq;
using Cloney.Core.Console;
using Cloney.Core.Diagnostics;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger if no arguments are
    /// provided. It starts the Cloney GUI and displays a
    /// short text in the console.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class GuiRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IProcess process;


        public GuiRoutine()
            : this(Default.Process, Default.Console, Default.Translator)
        {
        }

        public GuiRoutine(IProcess process, IConsole console, ITranslator translator)
            : base(null, console, translator)
        {
            this.process = process;
        }


        public bool Run(IEnumerable<string> args)
        {
            if (args.Count() > 0)
                return false;

            var message = Translator.Translate("GuiStartMessage");
            Console.WriteLine(message);
            process.Start("Cloney.Wizard.exe");
            return true;
        }
    }
}
