using System;
using System.Collections.Generic;
using System.IO;
using Cloney.Core.Console;
using Cloney.Core.ContextMenu;
using Cloney.Core.Localization;
using Cloney.Core.Reflection;

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

        private bool Run(IDictionary<string, string> args)
        {
            if (!HasSingleArg(args, "install", "true"))
                return false;

            var message = Translator.Translate("InstallMessage");
            var successMessage = Translator.Translate("InstallSuccessMessage");
            var errorMessage = Translator.Translate("InstallErrorMessage");
            var contextMenuText = Translator.Translate("ContextMenuText");

            try
            {
                var binDirectory = Assembly_FileExtensions.GetFilePathToExecutingAssembly();
                var applicationPath = Path.Combine(binDirectory, "Cloney.Wizard.exe");

                Console.WriteLine(message);
                installer.RegisterContextMenu(applicationPath, contextMenuText);
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