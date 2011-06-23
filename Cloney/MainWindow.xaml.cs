using System;
using System.Windows;
using Cloney.Domain.Cloning;

namespace Cloney
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            sourceFolderSelector.Initialize(new FolderNamespaceExtractor(), "");
            targetFolderSelector.Initialize(new FolderNamespaceExtractor(), "");
        }


        public bool CanInstall
        {
            get
            {
                return sourceFolderSelector.IsComplete && targetFolderSelector.IsComplete;
            }
        }


        private void folderSelector_OnChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.
            btnInstall.IsEnabled = CanInstall;
        }
    }
}
