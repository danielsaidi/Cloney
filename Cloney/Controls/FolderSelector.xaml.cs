using System;
using System.Windows.Controls;
using System.Windows.Forms;
using Cloney.Domain.Cloning.Abstractions;
using Cloney.Domain.Extensions;

namespace Cloney.Controls
{
    public partial class FolderSelector
    {
        public FolderSelector()
        {
            InitializeComponent();
        }


        public event EventHandler Changed;


        public bool IsValid
        {
            get { return !Path.IsNullOrEmpty() && !Namespace.IsNullOrEmpty(); }
        }

        public string Namespace
        {
            get { return txtNamespace.Text; }
            private set { txtNamespace.Text = value; }
        }

        public ICanExtractNamespace NamespaceExtractor { get; private set; }

        public string Path
        {
            get { return txtFolder.Text; }
            private set
            {
                txtFolder.Text = value;
                Namespace = NamespaceExtractor.ExtractNamespace(value);
            }
        }


        public void Initialize(ICanExtractNamespace namespaceExtractor, string initialFolderPath)
        {
            NamespaceExtractor = namespaceExtractor;
            Path = initialFolderPath;
            btnSelect.IsEnabled = NamespaceExtractor != null;
        }

        private void OnChanged(EventArgs e)
        {
            var handler = Changed;
            if (handler != null) handler(this, e);
        }


        private void btnSelect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog {SelectedPath = Path};
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            Path = folderBrowserDialog.SelectedPath;

            OnChanged(new EventArgs());
        }

        private void txtNamespace_Changed(object sender, TextChangedEventArgs e)
        {
            OnChanged(new EventArgs());
        }
    }
}
