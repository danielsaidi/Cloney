using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Cloney.Cloners;
using NExtra.Extensions;

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
                return sourceFolderSelector.IsValid && targetFolderSelector.IsValid && solutionCloner.CurrentPath.IsNullOrEmpty();
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
            if (!sourceFolderSelector.Path.IsNullOrEmpty())
                LastSourcePath = sourceFolderSelector.Path;
            if (!targetFolderSelector.Path.IsNullOrEmpty())
                LastTargetPath = targetFolderSelector.Path;

            btnClone.IsEnabled = CanInstall;
        }

        private void folderSelector_OnError(object sender, EventArgs e)
        {
            btnClone.IsEnabled = CanInstall;
        }

        void refreshTimer_Tick(object sender, EventArgs e)
        {
            btnClone.IsEnabled = CanInstall;
            lblCurrentPath.Content = solutionCloner.CurrentPath;
        }

        static void solutionCloner_CloningEnded(object sender, EventArgs e)
        {
            MessageBox.Show(Wizard.Resources.Language.CloningEndedMessage, Wizard.Resources.Language.CloningEndedTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            solutionCloner.CloneSolution(sourcePath, targetPath);
        }
    }
}
