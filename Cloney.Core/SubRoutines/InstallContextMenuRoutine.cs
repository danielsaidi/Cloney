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
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly IContextMenuInstaller installer;
        private readonly ICommandLineArgumentParser commandLineArgumentParser;


        public InstallContextMenuRoutine()
            :this(Default.Console, Default.Translator, Default.ContextMenuInstaller, Default.CommandLineArgumentParser)
        {
        }

        public InstallContextMenuRoutine(IConsole console, ITranslator translator, IContextMenuInstaller installer, ICommandLineArgumentParser commandLineArgumentParser)
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
            if (!HasSingleArg(args, "install", "true"))
                return false;

            try
            {
                console.WriteLine(translator.Translate("InstallMessage"));
                var binDirectory = Assembly_FileExtensions.GetFilePathToExecutingAssembly();
                var applicationPath = Path.Combine(binDirectory, "Cloney.Wizard.exe");
                installer.RegisterContextMenu(applicationPath, translator.Translate("ContextMenuText"));
                console.WriteLine(translator.Translate("InstallSuccessMessage"));
            }
            catch (Exception e)
            {
                console.WriteLine(translator.Translate("InstallErrorMessage"));
                console.WriteLine(e.Message);
            }

            return true;
        }
    }
}