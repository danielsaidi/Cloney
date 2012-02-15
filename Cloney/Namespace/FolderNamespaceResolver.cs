namespace Cloney.Namespace
{
    /// <summary>
    /// This class can be used to find the namespace of a
    /// solution, based on the name of a folder.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class FolderNamespaceResolver : INamespaceResolver
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
