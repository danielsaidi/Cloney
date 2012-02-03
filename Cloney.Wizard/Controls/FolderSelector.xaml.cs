using System;
using System.Windows.Controls;
using System.Windows.Forms;
using Cloney.Core.Namespace;
using NExtra.Extensions;

namespace Cloney.Wizard.Controls
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

        public INamespaceResolver NamespaceResolver { get; private set; }

        public string Path
        {
            get { return txtFolder.Text; }
            private set { txtFolder.Text = value; }
        }

        public bool ShowNamespaceTextbox
        {
            get { return txtNamespace.Visibility == System.Windows.Visibility.Visible; }
            set { txtNamespace.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
        }


        public void Initialize(INamespaceResolver namespaceResolver, string initialFolder)
        {
            NamespaceResolver = namespaceResolver;
            Path = initialFolder;
            btnSelect.IsEnabled = NamespaceResolver != null;
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
        }

        private void textBox_Changed(object sender, TextChangedEventArgs e)
        {
            Namespace = NamespaceResolver.ResolveNamespace(txtFolder.Text);
            OnChanged(new EventArgs());
        }
    }
}
