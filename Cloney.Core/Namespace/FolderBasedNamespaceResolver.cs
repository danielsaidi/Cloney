namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This class can be used to resolve the namespace
    /// of a solution based on the name of a folder. It
    /// is stupid and only looks at the folder name and
    /// does not handle folder hierarchies.
    /// </summary>
    public class FolderBasedNamespaceResolver : INamespaceResolver
    {
        public string ResolveNamespace(string folderPath)
        {
            folderPath = folderPath.Replace("/", "\\");
            folderPath = folderPath.EndsWith("\\") ? folderPath.TrimEnd('\\') : folderPath;

            var slashIndex = folderPath.LastIndexOf("\\") + 1;
            
            return folderPath.Substring(slashIndex, folderPath.Length - slashIndex);
        }
    }
}
