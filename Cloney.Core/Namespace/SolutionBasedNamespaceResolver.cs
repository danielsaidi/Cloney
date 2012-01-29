using System;
using System.IO;
using Cloney.Core.IO;

namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This class can be used to resolve the namespace
    /// of a solution based on the name of the solution
    /// file in a certain folder.
    /// </summary>
    public class SolutionBasedNamespaceResolver : INamespaceResolver
    {
        private readonly IDirectory directory;


        public SolutionBasedNamespaceResolver(IDirectory directory)
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
