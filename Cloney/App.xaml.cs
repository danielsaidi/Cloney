using System.Windows;
using System.Windows.Threading;

namespace Cloney
{
    public partial class App
    {
        public App()
        {
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
