using System.IO;
using Cloney.Core.IO;

namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This class can be used to get the namespace of a
    /// folder, based on its name.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class DirectoryNamespaceResolver : INamespaceResolver
    {
        private readonly IDirectory directory;


        public DirectoryNamespaceResolver(IDirectory directory)
        {
            this.directory = directory;
        }


        public string ResolveNamespace(string folderPath)
        {
            if (!directory.Exists(folderPath))
                throw new DirectoryNotFoundException();
            if (folderPath == string.Empty)
                throw new InvalidFolderException();

            folderPath = folderPath.Replace("/", "\\");
            folderPath = folderPath.EndsWith("\\") ? folderPath.TrimEnd('\\') : folderPath;

            var startIndex = folderPath.LastIndexOf("\\") + 1;
            
            return folderPath.Substring(startIndex, folderPath.Length - startIndex);
        }
    }
}
