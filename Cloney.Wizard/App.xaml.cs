using System.IO;
using System.Windows;
using System.Windows.Threading;
using Cloney.Core;
using Cloney.Core.Wizard;
using Cloney.Wizard.Resources;

namespace Cloney.Wizard
{
    /// <summary>
    /// The main Cloney Wizard application class.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public partial class App
    {
        public App()
        {
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //Arguments = Default.WizardApplicationCommandLineArgumentsParser.ParseCommandLineArguments(e.Args);
            //AdjustArguments(Arguments);

            base.OnStartup(e);
        }


        public static ApplicationArguments Arguments { get; private set; }


        private static void AdjustArguments(ApplicationArguments arguments)
        {
            /*if (arguments.SourcePath == null)
                return;

            if (Directory.Exists(arguments.SourcePath))
                return;

            var fileInfo = new FileInfo(arguments.SourcePath);
            if (fileInfo.Exists)
                arguments.SourcePath = fileInfo.Directory.FullName;*/
        }


        static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = string.Format(Language.UnhandledExceptionPattern, e.Exception.Message);
            MessageBox.Show(errorMessage, Language.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
