namespace Cloney.ContextMenu
{
    public interface IContextMenuInstaller
    {
        void RegisterContextMenu(string filePath);
        void UnregisterContextMenu();
    }
}