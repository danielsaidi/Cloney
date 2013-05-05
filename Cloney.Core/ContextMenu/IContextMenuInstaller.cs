namespace Cloney.Core.ContextMenu
{
    public interface IContextMenuInstaller
    {
        void RegisterContextMenu(string applicationFilePath, string menuItemText);
        void UnregisterContextMenu();
    }
}