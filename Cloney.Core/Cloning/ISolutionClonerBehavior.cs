namespace Cloney.Core.Cloning
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to determine how a certain path is to
    /// be cloned, how a namespace is to be replaced, and
    /// other atomic behavioral aspects, that has nothing
    /// to do with the actual file and folder shuffling.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface ISolutionClonerBehavior
    {
        bool ShouldExcludeFolder(string folderPath);
        bool ShouldExcludeFile(string filePath);
        bool ShouldPlainCopyFile(string filePath);
    }
}
