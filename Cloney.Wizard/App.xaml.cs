using System.Windows;
using System.Windows.Threading;
using Cloney.Core;
using Cloney.Core.Console;
using Cloney.Wizard.Resources;

namespace Cloney.Wizard
{
    /// <summary>
    /// The Cloney Wizard application class. This is the
    /// starting point when the wizard is launched.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public partial class App
    {
        private readonly ICommandLineArgumentParser argumentParser;


        public App()
            : this(Default.CommandLineArgumentParser)
        {
        }

        public App(ICommandLineArgumentParser argumentParser)
        {
            this.argumentParser = argumentParser;

            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            Arguments = argumentParser.ParseCommandLineArguments(e.Args);

            base.OnStartup(e);
        }


        public static CommandLineArguments Arguments { get; private set; }


        static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = string.Format(Language.UnhandledExceptionPattern, e.Exception.Message);
            MessageBox.Show(errorMessage, Language.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
