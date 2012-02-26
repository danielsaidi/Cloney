namespace Cloney.ContextMenu
{
    public interface IContextMenuInstaller
    {
        void RegisterContextMenu(string filePath, string menuText);
        void UnregisterContextMenu();
    }
}