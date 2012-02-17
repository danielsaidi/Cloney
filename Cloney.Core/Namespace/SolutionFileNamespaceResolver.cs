using System;
using System.IO;
using Cloney.Core.IO;

namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This class can be used to find the namespace of a
    /// solution, based on the solution file in a certain
    /// solution folder.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class SolutionFileNamespaceResolver : INamespaceResolver
    {
        private readonly IDirectory directory;


        public SolutionFileNamespaceResolver(IDirectory directory)
        {
            this.directory = directory;
        }


        public string ResolveNamespace(string folderPath)
        {
            foreach (var file in directory.GetFiles(folderPath))
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Extension != ".sln")
                    continue;

                return fileInfo.Name.Replace(fileInfo.Extension, "");
            }

            return String.Empty;
        }
    }
}
