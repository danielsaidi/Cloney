using System.IO;
using Microsoft.Win32;

namespace Cloney.ContextMenu
{
    public class ContextMenuInstaller : IContextMenuInstaller
    {
        private const string CloneyWizard = "Cloney.Wizard.exe";
        private const string RegistryKeyName = "VisualStudio.Launcher.sln";
        private const string ShellKeyName = "Cloney Context Menu";

        public void RegisterContextMenu(string filePath, string menuText)
        {
            CheckIfValid(filePath);

            string menuCommand = string.Format("\"{0}\\{1}\" \"%1\"", filePath, CloneyWizard);
            RegisterShellExtension(RegistryKeyName, ShellKeyName, menuText, menuCommand);
        }

        public void UnregisterContextMenu()
        {
            var key = Registry.ClassesRoot.OpenSubKey(string.Format(@"{0}\Shell\{1}", RegistryKeyName, ShellKeyName));

            if (key != null)
                UnregisterShellExtension(RegistryKeyName, ShellKeyName);
        }

        private static void CheckIfValid(string filePath)
        {
            var dir = new DirectoryInfo(filePath);
            if (!dir.Exists)
                throw new FileNotFoundException(string.Format("Could not find the directory: {0}", filePath));

            if(dir.GetFiles(CloneyWizard).Length == 0)
                throw new FileNotFoundException(string.Format("Could not find Cloney.Wizard.exe in the directory: {0} ", filePath));
        }

        private static void RegisterShellExtension(string fileType, string shellKeyName, string menuText, string menuCommand)
        {
            string regPath = string.Format(@"{0}\shell\{1}", fileType, shellKeyName);

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(regPath))
            {
                if (key == null)
                    throw new RegistryException("Could not create subkey under HKEY_CLASSES_ROOT");

                key.SetValue(null, menuText);
            }

            // add command which calls Cloney.Wizard with command line arg to the registry
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(string.Format(@"{0}\command", regPath)))
            {
                if (key == null)
                    throw new RegistryException("Could not create subkey under HKEY_CLASSES_ROOT");

                key.SetValue(null, menuCommand);
            }
        }

        private static void UnregisterShellExtension(string fileType, string shellKeyName)
        {
            string regPath = string.Format(@"{0}\shell\{1}", fileType, shellKeyName);

            Registry.ClassesRoot.DeleteSubKeyTree(regPath);
        }
    }
}