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
        private readonly IContextMenuInstaller installer;


        public UninstallContextMenuRoutine()
            : this(Default.ContextMenuInstaller, Default.CommandLineArgumentParser, Default.Console, Default.Translator)
        {
        }

        public UninstallContextMenuRoutine(IContextMenuInstaller installer, ICommandLineArgumentParser argumentParser, IConsole console, ITranslator translator)
            : base(argumentParser, console, translator)
        {
            this.installer = installer;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(ArgumentParser.ParseCommandLineArguments(args));
        }

        private bool Run(CommandLineArguments args)
        {
            if (!args.HasSingleArgument("uninstall", "true"))
                return false;

            var message = Translator.Translate("UninstallMessage");
            var successMessage = Translator.Translate("UninstallSuccessMessage");
            var errorMessage = Translator.Translate("UninstallErrorMessage");

            try
            {
                Console.WriteLine(message);
                installer.UnregisterContextMenu();
                Console.WriteLine(successMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine(e.Message);
            }

            return true;
        }
    }
}