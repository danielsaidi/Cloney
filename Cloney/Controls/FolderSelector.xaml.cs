using System;
using System.Windows.Controls;
using System.Windows.Forms;
using Cloney.Domain.Cloning.Abstractions;
using Cloney.Domain.Extensions;

namespace Cloney.Controls
{
    /// <summary>
    /// Interaction logic for FolderSelector.xaml
    /// </summary>
    public partial class FolderSelector
    {
        private ICanExtractFolderNamespace _folderNamespaceExtractor;


        public FolderSelector()
        {
            InitializeComponent();
        }


        public event EventHandler Changed;


        public void Initialize(ICanExtractFolderNamespace folderFolderNamespaceExtractor, string initialFolderPath)
        {
            _folderNamespaceExtractor = folderFolderNamespaceExtractor;
            FolderPath = initialFolderPath;

            btnSelect.IsEnabled = _folderNamespaceExtractor != null;
        }

        public void OnChanged(EventArgs e)
        {
            var handler = Changed;
            if (handler != null) handler(this, e);
        }


        public string FolderPath { get { return txtFolder.Text; } set { txtFolder.Text = value; } }

        public bool IsComplete { get { return !FolderPath.IsNullOrEmpty() && !Namespace.IsNullOrEmpty(); } }

        public string Namespace { get { return txtNamespace.Text; } set { txtNamespace.Text = value; } }


        private void btnSelect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog {SelectedPath = FolderPath};
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            FolderPath = folderBrowserDialog.SelectedPath;
            Namespace = _folderNamespaceExtractor.ExtractFolderNamespace(FolderPath);

            OnChanged(new EventArgs());
        }

        private void txtNamespace_Changed(object sender, TextChangedEventArgs e)
        {
            OnChanged(new EventArgs());
        }
    }
}
