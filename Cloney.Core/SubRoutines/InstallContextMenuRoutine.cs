using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Cloney.ContextMenu;
using Cloney.Core.Console;
using Cloney.Core.Localization;

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
        private readonly IArgumentParser<IDictionary<string, string>> argumentParser;


        public InstallContextMenuRoutine()
            :this(Default.Console, Default.Translator, new ContextMenuInstaller())
        {
        }

        public InstallContextMenuRoutine(IConsole console, ITranslator translator, IContextMenuInstaller installer)
        {
            this.console = console;
            this.translator = translator;
            this.installer = installer;

            argumentParser = Default.DictionaryArgumentParser;
        }


        public bool Run(IEnumerable<string> args)
        {
            return Run(argumentParser.ParseArguments(args));
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
            
            var assemblyFile = Assembly.GetExecutingAssembly().Location;
            var assemblyFolder = new FileInfo(assemblyFile).Directory.ToString();

            installer.RegisterContextMenu(assemblyFolder, translator.Translate("ContextMenuText"));
            console.WriteLine(string.Format(translator.Translate("SuccessfulInstallMessage"), assemblyFolder));
        }
    }
}