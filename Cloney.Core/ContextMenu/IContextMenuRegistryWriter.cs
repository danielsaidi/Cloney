namespace Cloney.Core.ContextMenu
{
    public interface IContextMenuRegistryWriter
    {
        void RegisterShellExtension(string registryPath, string menuText, string command);
        void UnregisterShellExtension(string registryPath);
    }
}
