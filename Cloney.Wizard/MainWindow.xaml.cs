using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Cloney.Core;
using Cloney.Core.Cloners;

namespace Cloney.Wizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public partial class MainWindow
    {
        private ISolutionCloner solutionCloner;
        private DispatcherTimer refreshTimer;

        private string sourcePath;
        private string targetPath;


        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }


        public bool CanClone
        {
            get
            {
                return sourceFolderSelector.IsValid && targetFolderSelector.IsValid && string.IsNullOrEmpty(solutionCloner.CurrentPath);
            }
        }

        public string LastSourcePath
        {
            get { return Properties.Settings.Default.LastSourcePath; }
            set
            {
                Properties.Settings.Default.LastSourcePath = value;
                Properties.Settings.Default.Save();
            }
        }

        public string LastTargetPath
        {
            get { return Properties.Settings.Default.LastTargetPath; }
            set
            {
                Properties.Settings.Default.LastTargetPath = value;
                Properties.Settings.Default.Save();
            }
        }


        private void Initialize()
        {
            InitializeSolutionCloner();
            InitializeTimer();
            InitializeFolderSelectors();
            InitializeModalBehavior();
        }

        private void InitializeModalBehavior()
        {
            if (!App.Arguments.ModalMode)
                return;

            Hide();

            if (string.IsNullOrEmpty(App.Arguments.SourcePath))
                if (sourceFolderSelector.ShowModal(Wizard.Resources.Language.SelectSourceFolder) != System.Windows.Forms.DialogResult.OK)
                    return;

            if (targetFolderSelector.ShowModal(Wizard.Resources.Language.SelectTargetFolder) != System.Windows.Forms.DialogResult.OK)
                return;

            StartCloningOperation();
        }

        private void InitializeFolderSelectors()
        {
            var initialSourcePath = App.Arguments.SourcePath ?? LastSourcePath;

            sourceFolderSelector.Initialize(Default.SourceFolderNamespaceResolver, initialSourcePath);
            targetFolderSelector.Initialize(Default.TargetNamespaceResolver, LastTargetPath);
        }

        private void InitializeSolutionCloner()
        {
            solutionCloner = Default.SolutionCloner;
        }

        private void InitializeTimer()
        {
            refreshTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 10) };
            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.IsEnabled = true;
            refreshTimer.Start();
        }

        private void Refresh()
        {
            lblCurrentPath.Content = string.Empty;

            if (!sourceFolderSelector.IsValid)
                lblCurrentPath.Content = Wizard.Resources.Language.InvalidSourceFolder;

            if (!sourceFolderSelector.IsValid)
                lblCurrentPath.Content = Wizard.Resources.Language.InvalidSourceFolder;

            btnClone.IsEnabled = CanClone;
        }


        private void StartCloningOperation()
        {
            if (!CanClone)
                return;

            sourcePath = sourceFolderSelector.Path;
            targetPath = targetFolderSelector.Path;

            Topmost = true;
            Show();

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }


        private void btnClone_Click(object sender, RoutedEventArgs e)
        {
            StartCloningOperation();
        }

        private void folderSelector_OnChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sourceFolderSelector.Path))
                LastSourcePath = sourceFolderSelector.Path;
            if (!string.IsNullOrEmpty(targetFolderSelector.Path))
                LastTargetPath = targetFolderSelector.Path;

            Refresh();
        }

        private void folderSelector_OnError(object sender, EventArgs e)
        {
            Refresh();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            btnClone.IsEnabled = CanClone;
            lblCurrentPath.Content = solutionCloner.CurrentPath;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            solutionCloner.CloneSolution(sourcePath, targetPath);
        }

        static void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(Wizard.Resources.Language.CloningEndedMessage, Wizard.Resources.Language.CloningEndedTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            if (App.Arguments.ModalMode)
                Application.Current.Shutdown();
        }
    }
}
