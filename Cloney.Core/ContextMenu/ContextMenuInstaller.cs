using System.IO;
using Cloney.Core.IO;

namespace Cloney.Core.ContextMenu
{
    /// <summary>
    /// This class will start the Cloney program that is
    /// defined in the Cloney.Core library.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class ContextMenuInstaller : IContextMenuInstaller
    {
        private readonly IFile file;
        private readonly IContextMenuRegistryWriter registryWriter;


        public ContextMenuInstaller(IFile file, IContextMenuRegistryWriter registryWriter)
        {
            this.file = file;
            this.registryWriter = registryWriter;
        }


        public string RegistryPath
        {
            get
            {
                const string registryKeyName = "VisualStudio.Launcher.sln";
                const string shellKeyName = "Cloney Context Menu";
                return string.Format(@"{0}\shell\{1}", registryKeyName, shellKeyName);
            }
        }


        public void RegisterContextMenu(string applicationFilePath, string menuItemText)
        {
            if (!file.Exists(applicationFilePath))
                throw new FileNotFoundException("The application {0} does not exist.", applicationFilePath);
            
            const string commandArgs = "--source=\"%1\" --modal";
            var command = string.Format("\"{0}\" {1}", applicationFilePath, commandArgs);

            registryWriter.RegisterShellExtension(RegistryPath, menuItemText, command);
        }

        public void UnregisterContextMenu()
        {
            registryWriter.UnregisterShellExtension(RegistryPath);
        }
    }
}
