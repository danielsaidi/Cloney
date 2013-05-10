using System;
using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.ContextMenu;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine uninstalls the Cloney context menu.
    /// It is triggered by the "cloney --uninstall" command.
    /// </summary>
    public class UninstallContextMenuRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly IContextMenuInstaller installer;
        private readonly ICommandLineArgumentParser commandLineArgumentParser;


        public UninstallContextMenuRoutine()
            : this(Default.Console, Default.Translator, Default.ContextMenuInstaller, Default.CommandLineArgumentParser)
        {
        }

        public UninstallContextMenuRoutine(IConsole console, ITranslator translator, IContextMenuInstaller installer, ICommandLineArgumentParser commandLineArgumentParser)
        {
            this.console = console;
            this.translator = translator;
            this.installer = installer;
            this.commandLineArgumentParser = commandLineArgumentParser;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(commandLineArgumentParser.ParseCommandLineArguments(args));
        }

        private bool Run(IDictionary<string, string> args)
        {
            if (!HasSingleArg(args, "uninstall", "true"))
                return false;

            try
            {
                console.WriteLine(translator.Translate("UninstallMessage"));
                installer.UnregisterContextMenu();
                console.WriteLine(translator.Translate("UninstallSuccessMessage"));
            }
            catch (Exception e)
            {
                console.WriteLine(translator.Translate("UninstallErrorMessage"));
                console.WriteLine(e.Message);
            }

            return true;
        }
    }
}