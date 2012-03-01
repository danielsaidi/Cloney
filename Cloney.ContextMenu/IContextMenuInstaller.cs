namespace Cloney.ContextMenu
{
    public interface IContextMenuInstaller
    {
        void RegisterContextMenu(string applicationFolder, string menuText);
        void UnregisterContextMenu();
    }
}