using System;
using Microsoft.Win32;

namespace Cloney.Core.ContextMenu
{
    public class ContextMenuRegistryWriter : IContextMenuRegistryWriter
    {
        public void RegisterShellExtension(string registryPath, string menuText, string command)
        {
            RegisterShellExtensionKey(registryPath, menuText);
            RegisterShellExtensionKeyCommand(registryPath, command);
        }

        public void UnregisterShellExtension(string registryPath)
        {
            var key = Registry.ClassesRoot.OpenSubKey(registryPath);
            if (key == null)
                return;

            Registry.ClassesRoot.DeleteSubKeyTree(registryPath);
        }


        private static void RegisterShellExtensionKey(string registryPath, string menuText)
        {
            using (var key = Registry.ClassesRoot.CreateSubKey(registryPath))
            {
                if (key == null)
                    throw new RegistryException("Could not create extension key under HKEY_CLASSES_ROOT");

                key.SetValue(null, menuText);
            }
        }

        private static void RegisterShellExtensionKeyCommand(string registryPath, string menuCommand)
        {
            using (var key = Registry.ClassesRoot.CreateSubKey(string.Format(@"{0}\command", registryPath)))
            {
                if (key == null)
                    throw new RegistryException("Could not create extension key command under HKEY_CLASSES_ROOT");

                key.SetValue(null, menuCommand);
            }
        }
    }
}