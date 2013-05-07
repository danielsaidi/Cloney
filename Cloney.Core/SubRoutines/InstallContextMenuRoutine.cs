using System;
using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.ContextMenu;
using Cloney.Core.Localization;
using Cloney.Core.Reflection;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine triggers on this console command:
    /// cloney --install
    /// When triggered, it installs the convenient Cloney
    /// Windows Explorer plugin.
    /// </summary>
    public class InstallContextMenuRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly IContextMenuInstaller installer;
        private readonly ICommandLineArgumentParser<IDictionary<string, string>> commandLineArgumentParser;


        public InstallContextMenuRoutine()
            :this(Default.Console, Default.Translator, new ContextMenuInstaller(Default.File, Default.ContextMenuRegistryWriter))
        {
        }

        public InstallContextMenuRoutine(IConsole console, ITranslator translator, IContextMenuInstaller installer)
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
            if (!HasSingleArg(args, "install", "true"))
                return false;

            try
            {
                RunInstall();
            }
            catch (Exception e)
            {
                console.WriteLine(translator.Translate("InstallerErrorMessage"));
                console.WriteLine(e.Message);
            }

            return true;
        }

        private void RunInstall()
        {
            console.WriteLine(translator.Translate("InstallMessage"));
            var binDirectory = Assembly_FileExtensions.GetFilePathToExecutingAssembly();
            installer.RegisterContextMenu(binDirectory, translator.Translate("ContextMenuText"));
            console.WriteLine("\n" + translator.Translate("SuccessfulInstallMessage"));
        }
    }
}