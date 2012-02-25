using System;
using System.Collections.Generic;
using Cloney.ContextMenu;
using Cloney.Core.Console;
using Cloney.Core.Localization;
using Cloney.Core.Reflection;

namespace Cloney.Core.SubRoutines
{
    public class InstallContextMenuRoutine : SubRoutineBase, ISubRoutine
    {
        private readonly IConsole console;
        private readonly ITranslator translator;
        private readonly IContextMenuInstaller installer;

        public InstallContextMenuRoutine()
            :this(Default.Console, Default.Translator, new ContextMenuInstaller())
        {
        }

        public InstallContextMenuRoutine(IConsole console, ITranslator translator, IContextMenuInstaller installer)
        {
            this.console = console;
            this.translator = translator;
            this.installer = installer;
        }

        public bool Run(IDictionary<string, string> args)
        {
            bool runInstall = ArgsHaveSingleKeyValue(args, "install", "true");
            bool runUninstall = ArgsHaveSingleKeyValue(args, "uninstall", "true");

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
            string binDirectory = AssemblyExtensions.GetFilePathToAssemblyCodeBase();
            installer.RegisterContextMenu(binDirectory);
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