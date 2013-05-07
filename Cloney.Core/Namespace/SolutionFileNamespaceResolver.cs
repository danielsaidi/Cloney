using System.IO;
using Cloney.Core.IO;

namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This class can be used to get the namespace of a
    /// solution file, based on its name.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class SolutionFileNamespaceResolver : INamespaceResolver
    {
        private readonly IFile file;


        public SolutionFileNamespaceResolver(IFile file)
        {
            this.file = file;
        }


        public string ResolveNamespace(string filePath)
        {
            if (!file.Exists(filePath))
                throw new FileNotFoundException();

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Extension != ".sln")
                throw new InvalidSolutionFileException();

            return fileInfo.Name.Replace(fileInfo.Extension, "");
        }
    }
}
