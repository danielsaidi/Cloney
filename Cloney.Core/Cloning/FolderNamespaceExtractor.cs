using System;
using Cloney.Core.Cloning.Abstractions;
using Cloney.Core.Extensions;

namespace Cloney.Core.Cloning
{
    public class FolderNamespaceExtractor : ICanExtractNamespace
    {
        public string ExtractNamespace(string folderPath)
        {
            if (folderPath.IsNullOrEmpty())
                return String.Empty;

            var slashIndex = folderPath.LastIndexOf("\\") + 1;
            
            return folderPath.Substring(slashIndex, folderPath.Length - slashIndex);
        }
    }
}
