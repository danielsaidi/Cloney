using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Cloney.Core.Namespace;

namespace Cloney.Wizard.Controls
{
    /// <summary>
    /// This component can be used to select a folder by
    /// using a native folder selector.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public partial class FolderSelector
    {
        private readonly Brush errorBrush;
        private readonly Brush neutralBrush;


        public FolderSelector()
        {
            errorBrush = new SolidColorBrush(Color.FromRgb(255, 200, 200));
            neutralBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            InitializeComponent();
        }


        public event EventHandler Changed;

        public event EventHandler Error;


        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(Namespace); }
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
            get { return txtNamespace.Visibility == Visibility.Visible; }
            set { txtNamespace.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
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

        private void OnError(EventArgs e)
        {
            var handler = Error;
            if (handler != null) handler(this, e);
        }


        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog {SelectedPath = Path};
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            Path = folderBrowserDialog.SelectedPath;
        }

        private void textBox_Changed(object sender, TextChangedEventArgs e)
        {
            ResolveNamespace();
        }

        private void txtFolder_LostFocus(object sender, RoutedEventArgs e)
        {
            ResolveNamespace();
        }

        private void ResolveNamespace()
        {
            try
            {
                Namespace = NamespaceResolver.ResolveNamespace(txtFolder.Text);
                if (string.IsNullOrEmpty(Namespace))
                    throw new Exception(txtFolder.Text);

                txtFolder.Background = neutralBrush;
                OnChanged(new EventArgs());
            }
            catch
            {
                txtFolder.Background = errorBrush;
                OnError(new EventArgs());
            }
        }
    }
}
