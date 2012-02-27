using System;
using System.Collections.Generic;
using Cloney.ContextMenu;
using Cloney.Core.Console;
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
        private readonly ICommandLineArgumentParser argumentParser;

        public InstallContextMenuRoutine()
            :this(Default.Console, Default.Translator, new ContextMenuInstaller())
        {
        }

        public InstallContextMenuRoutine(IConsole console, ITranslator translator, IContextMenuInstaller installer)
        {
            this.console = console;
            this.translator = translator;
            this.installer = installer;

            argumentParser = Default.CommandLineArgumentParser;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(argumentParser.ParseCommandLineArguments(args));
        }

        private bool Run(IDictionary<string, string> args)
        {
            var runInstall = HasSingleArg(args, "install", "true");
            var runUninstall = HasSingleArg(args, "uninstall", "true");

            if (!runInstall && !runUninstall)
                return false;

            try
            {
                if (runInstall)
                {
                    RunInstall();
                }
                else
                {
                    RunUninstall();
                }
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
            var binDirectory = AssemblyExtensions.GetFilePathToAssemblyCodeBase();
            installer.RegisterContextMenu(binDirectory, translator.Translate("ContextMenuText"));
            console.WriteLine("\n" + translator.Translate("SuccessfulInstallMessage"));
        }

        private void RunUninstall()
        {
            console.WriteLine(translator.Translate("UninstallMessage"));
            installer.UnregisterContextMenu();
            console.WriteLine("\n" + translator.Translate("SuccessfulUninstallMessage"));
        }
    }
}