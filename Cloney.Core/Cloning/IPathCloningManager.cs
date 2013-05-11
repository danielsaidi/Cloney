namespace Cloney.Core.Cloning
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to determine how a certain path is to
    /// be cloned.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface IPathCloningManager
    {
        bool ShouldExcludeFolder(string folderPath);
        bool ShouldExcludeFile(string filePath);
        bool ShouldPlainCopyFile(string filePath);
    }
}
