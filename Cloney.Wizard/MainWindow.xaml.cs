using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Cloney.Core;
using Cloney.Core.Cloning;
using Cloney.Core.Namespace;
using Cloney.Wizard.Controls;
using Cloney.Wizard.Properties;

namespace Cloney.Wizard
{
    /// <summary>
    /// This class defines the interaction logic for the
    /// main Cloney Wizard window.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public partial class MainWindow
    {
        private readonly ISolutionCloner solutionCloner;
        private DispatcherTimer refreshTimer;


        public MainWindow()
            : this(Default.SolutionCloner, Default.SourceNamespaceResolver, Default.TargetNamespaceResolver)
        {
        }

        public MainWindow(ISolutionCloner solutionCloner, INamespaceResolver sourceNamespaceResolver, INamespaceResolver targetNamespaceResolver)
        {
            InitializeComponent();

            this.solutionCloner = solutionCloner;
            InitializeTimer();
            InitializePathSelectors(sourceNamespaceResolver, targetNamespaceResolver);

            Refresh();
            if (App.IsModal)
                StartAsModal();
        }


        public bool CanClone
        {
            get
            {
                var validSource = sourcePathSelector.HasValidPath;
                var validTarget = targetPathSelector.HasValidPath;
                var notCloning  = string.IsNullOrEmpty(solutionCloner.CurrentPath);
                return validSource && validTarget && notCloning;
            }
        }

        public string LastSourcePath
        {
            get { return Settings.Default.LastSourcePath; }
            set
            {
                Settings.Default.LastSourcePath = value;
                Settings.Default.Save();
            }
        }

        public string LastTargetPath
        {
            get { return Settings.Default.LastTargetPath; }
            set
            {
                Settings.Default.LastTargetPath = value;
                Settings.Default.Save();
            }
        }


        private void InitializePathSelectors(INamespaceResolver sourceNamespaceResolver, INamespaceResolver targetNamespaceResolver)
        {
            sourcePathSelector.Initialize(sourceNamespaceResolver, PathType.File, App.InputSource ?? LastSourcePath);
            sourcePathSelector.DialogTitle = Wizard.Resources.Language.SelectSource;
            targetPathSelector.Initialize(targetNamespaceResolver, PathType.Folder, LastTargetPath);
            targetPathSelector.DialogTitle = Wizard.Resources.Language.SelectTarget;
        }

        private void InitializeTimer()
        {
            refreshTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 10) };
            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.Stop();
        }

        private void Refresh()
        {
            lblCurrentPath.Content = string.Empty;
            if (!sourcePathSelector.HasValidPath)
                lblCurrentPath.Content = Wizard.Resources.Language.InvalidSource;
            else if (!targetPathSelector.HasValidPath)
                lblCurrentPath.Content = Wizard.Resources.Language.InvalidTarget;

            btnClone.IsEnabled = CanClone;
        }

        private static void Shutdown()
        {
            Application.Current.Shutdown();
        }

        private static void Shutdown(string exitTitle, string exitMessage)
        {
            MessageBox.Show(exitMessage, exitTitle);
            Shutdown();
        }

        private void StartAsModal()
        {
            Hide();

            if (string.IsNullOrWhiteSpace(App.InputSource))
                if (sourcePathSelector.OpenPathSelector() != System.Windows.Forms.DialogResult.OK)
                {
                    Shutdown();
                    return;
                }

            if (!sourcePathSelector.HasValidPath)
            {
                Shutdown(Wizard.Resources.Language.Error, Wizard.Resources.Language.InvalidSource);
                return;
            }

            if (targetPathSelector.OpenPathSelector() != System.Windows.Forms.DialogResult.OK)
            {
                Shutdown();
                return;
            }

            if (!targetPathSelector.HasValidPath)
            {
                Shutdown(Wizard.Resources.Language.Error, Wizard.Resources.Language.InvalidTarget);
                return;
            }

            StartCloningOperation();
        }

        private void StartCloningOperation()
        {
            if (!CanClone)
                return;

            Topmost = true;
            Show();
            refreshTimer.Start();

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }


        private void btnClone_Click(object sender, RoutedEventArgs e)
        {
            StartCloningOperation();
        }

        private void pathSelector_OnChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sourcePathSelector.Path))
                LastSourcePath = sourcePathSelector.Path;
            if (!string.IsNullOrEmpty(targetPathSelector.Path))
                LastTargetPath = targetPathSelector.Path;

            Refresh();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            btnClone.IsEnabled = CanClone;
            lblCurrentPath.Content = solutionCloner.CurrentPath;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            solutionCloner.CloneSolution(sourcePathSelector.Path, targetPathSelector.Path);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshTimer.Stop();
            MessageBox.Show(Wizard.Resources.Language.CloningEndedMessage, Wizard.Resources.Language.CloningEndedTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            if (App.IsModal)
                Shutdown();
        }
    }
}
