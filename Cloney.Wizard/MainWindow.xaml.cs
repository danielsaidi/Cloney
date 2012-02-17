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
            InitializeBootstrap();
        }


        public bool CanInstall
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


        private void InitializeBootstrap()
        {
            solutionCloner = Default.SolutionCloner;
            solutionCloner.CloningEnded += solutionCloner_CloningEnded;

            refreshTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 10) };
            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.IsEnabled = true;
            refreshTimer.Start();

            sourceFolderSelector.Initialize(Default.SourceNamespaceResolver, LastSourcePath);
            targetFolderSelector.Initialize(Default.TargetNamespaceResolver, LastTargetPath);
        }

        private void Refresh()
        {
            lblCurrentPath.Content = string.Empty;

            if (!sourceFolderSelector.IsValid)
                lblCurrentPath.Content = Wizard.Resources.Language.InvalidSourceFolder;

            if (!sourceFolderSelector.IsValid)
                lblCurrentPath.Content = Wizard.Resources.Language.InvalidSourceFolder;

            btnClone.IsEnabled = CanInstall;
        }


        private void btnClone_Click(object sender, RoutedEventArgs e)
        {
            sourcePath = sourceFolderSelector.Path;
            targetPath = targetFolderSelector.Path;

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
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
            btnClone.IsEnabled = CanInstall;
            lblCurrentPath.Content = solutionCloner.CurrentPath;
        }

        private static void solutionCloner_CloningEnded(object sender, EventArgs e)
        {
            MessageBox.Show(Wizard.Resources.Language.CloningEndedMessage, Wizard.Resources.Language.CloningEndedTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            solutionCloner.CloneSolution(sourcePath, targetPath);
        }
    }
}
