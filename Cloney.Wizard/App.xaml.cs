using System.Windows;
using System.Windows.Threading;
using Cloney.Core;
using Cloney.Core.Console;
using Cloney.Wizard.Resources;

namespace Cloney.Wizard
{
    /// <summary>
    /// This is the Cloney Wizard application class. The
    /// class is the starting point when the application
    /// is launched.
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

        public static string InputSource
        {
            get { return Arguments.HasArgument("source") ? Arguments.Raw["source"] : null; }
        }

        public static bool IsModal
        {
            get { return Arguments.HasArgument("modal", "true"); }
        }


        static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = string.Format(Language.UnhandledExceptionPattern, e.Exception.Message);
            MessageBox.Show(errorMessage, Language.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
