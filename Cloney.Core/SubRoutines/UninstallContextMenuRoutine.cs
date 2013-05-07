using System;
using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.ContextMenu;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on this console command:
    /// cloney --uninstall
    /// When triggered, it uninstalls and unregisters the
    /// Cloney Windows Explorer plugin.
    /// </summary>
    public class UninstallContextMenuRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly IContextMenuInstaller installer;
        private readonly ICommandLineArgumentParser<IDictionary<string, string>> commandLineArgumentParser;


        public UninstallContextMenuRoutine()
            :this(Default.Console, Default.Translator, new ContextMenuInstaller(Default.File, Default.ContextMenuRegistryWriter))
        {
        }

        public UninstallContextMenuRoutine(IConsole console, ITranslator translator, IContextMenuInstaller installer)
        {
            this.console = console;
            this.translator = translator;
            this.installer = installer;

            commandLineArgumentParser = Default.CommandLineArgumentParser;
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
                RunUninstall();
            }
            catch (Exception e)
            {
                console.WriteLine(translator.Translate("InstallerErrorMessage"));
                console.WriteLine(e.Message);
            }

            return true;
        }

        private void RunUninstall()
        {
            console.WriteLine(translator.Translate("UninstallMessage"));
            installer.UnregisterContextMenu();
            console.WriteLine("\n" + translator.Translate("SuccessfulUninstallMessage"));
        }
    }
}