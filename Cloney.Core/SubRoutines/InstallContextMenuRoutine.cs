using System;
using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.ContextMenu;
using Cloney.Core.Localization;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine installs the Cloney context menu.
    /// It is triggered by the "cloney --install" command.
    /// </summary>
    public class InstallContextMenuRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IContextMenuInstaller installer;


        public InstallContextMenuRoutine()
            : this(Default.ContextMenuInstaller, Default.CommandLineArgumentParser, Default.Console, Default.Translator)
        {
        }

        public InstallContextMenuRoutine(IContextMenuInstaller installer, ICommandLineArgumentParser argumentParser, IConsole console, ITranslator translator)
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
            if (!args.HasSingleArgument("install", "true"))
                return false;

            var message = Translator.Translate("InstallMessage");
            var successMessage = Translator.Translate("InstallSuccessMessage");
            var errorMessage = Translator.Translate("InstallErrorMessage");
            var contextMenuText = Translator.Translate("ContextMenuText");

            try
            {
                Console.WriteLine(message);
                installer.RegisterContextMenu(WizardApplicationPath, contextMenuText);
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