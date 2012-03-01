using System.IO;
using Microsoft.Win32;

namespace Cloney.ContextMenu
{
    public class ContextMenuInstaller : IContextMenuInstaller
    {
        private const string executable = "Cloney.Wizard.exe";
        private const string registryKeyName = "VisualStudio.Launcher.sln";
        private const string shellKeyName = "Cloney Context Menu";


        public void RegisterContextMenu(string applicationFolder, string menuText)
        {
            ValidateApplication(applicationFolder);

            var menuCommand = string.Format("\"{0}\\{1}\" --source=\"%1\" --modal", applicationFolder, executable);
            
            RegisterShellExtension(registryKeyName, shellKeyName, menuText, menuCommand);
        }

        public void UnregisterContextMenu()
        {
            var key = Registry.ClassesRoot.OpenSubKey(string.Format(@"{0}\Shell\{1}", registryKeyName, shellKeyName));
            if (key == null)
                return;

            UnregisterShellExtension(registryKeyName, shellKeyName);
        }


        private static void RegisterShellExtension(string fileType, string shellKey, string menuText, string menuCommand)
        {
            var regPath = string.Format(@"{0}\shell\{1}", fileType, shellKey);

            RegisterShellExtensionKey(regPath, menuText);
            RegisterShellExtensionKeyCommand(regPath, menuCommand);
        }

        private static void RegisterShellExtensionKey(string regPath, string menuText)
        {
            using (var key = Registry.ClassesRoot.CreateSubKey(regPath))
            {
                if (key == null)
                    throw new RegistryException("Could not create subkey under HKEY_CLASSES_ROOT");

                key.SetValue(null, menuText);
            }
        }

        private static void RegisterShellExtensionKeyCommand(string regPath, string menuCommand)
        {
            using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"{0}\command", regPath)))
            {
                if (key == null)
                    throw new RegistryException("Could not create subkey under HKEY_CLASSES_ROOT");

                key.SetValue(null, menuCommand);
            }
        }

        private static void UnregisterShellExtension(string fileType, string shellKey)
        {
            var regPath = string.Format(@"{0}\shell\{1}", fileType, shellKey);

            Registry.ClassesRoot.DeleteSubKeyTree(regPath);
        }

        private static void ValidateApplication(string applicationFolder)
        {
            var dirInfo = new DirectoryInfo(applicationFolder);

            if (!dirInfo.Exists)
                throw new FileNotFoundException(string.Format("Could not find the directory: {0}", dirInfo));

            if (dirInfo.GetFiles(executable).Length == 0)
                throw new FileNotFoundException(string.Format("Could not find cloney.exe in the directory: {0} ", dirInfo));
        }
    }
}