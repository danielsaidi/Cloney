using System;
using System.IO;
using System.Windows.Forms;
using Cloney.Core.Namespace;
using UserControl = System.Windows.Controls.UserControl;

namespace Cloney.Wizard.Controls
{
    public class PathSelectorBase : UserControl
    {
        public event EventHandler PathChanged;


        public string DialogTitle { get; set; }

        public bool HasNamespace
        {
            get { return !string.IsNullOrWhiteSpace(Namespace); }
        }

        public bool HasPath
        {
            get { return !string.IsNullOrWhiteSpace(Path); }
        }

        public bool HasValidPath
        {
            get { return HasPath && HasNamespace; }
        }

        public string Namespace { get; private set; }

        protected INamespaceResolver NamespaceResolver { get; private set; }

        public string Path { get; private set; }

        public PathType PathType { get; set; }

        public void Initialize(INamespaceResolver namespaceResolver, PathType pathType)
        {
            Initialize(namespaceResolver, pathType, null);
        }

        public void Initialize(INamespaceResolver namespaceResolver, PathType pathType, string initialPath)
        {
            NamespaceResolver = namespaceResolver;
            PathType = pathType;
            SetPath(initialPath);
        }

        public DialogResult OpenPathSelector()
        {
            var result = DialogResult.Cancel;

            if (PathType == PathType.File)
                result = OpenPathSelectorForFile();
            if (PathType == PathType.Folder)
                result = OpenPathSelectorForFolder();

            if (result != DialogResult.Cancel)
                SetPath(Path);

            return result;
        }

        private DialogResult OpenPathSelectorForFile()
        {
            var dialog = new OpenFileDialog();
            if (File.Exists(Path))
                dialog.InitialDirectory = new FileInfo(Path).ToString();
            if (DialogTitle != null)
                dialog.Title = DialogTitle;

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                Path = dialog.FileName;

            return result;
        }

        private DialogResult OpenPathSelectorForFolder()
        {
            var dialog = new FolderBrowserDialog { SelectedPath = Path };
            if (DialogTitle != null)
                dialog.Description = DialogTitle;

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                Path = dialog.SelectedPath;

            return result;
        }

        protected virtual void Refresh()
        {
        }

        protected void ResolveNamespace()
        {
            try
            {
                Namespace = NamespaceResolver.ResolveNamespace(Path);
            }
            catch
            {
                Namespace = null;
            }
        }

        public void SetPath(string path)
        {
            Path = path;
            ResolveNamespace();
            Refresh();
            OnPathChanged(new EventArgs());
        }


        protected void OnPathChanged(EventArgs e)
        {
            var handler = PathChanged;
            if (handler != null)
                handler(this, e);
        }
    }
}
