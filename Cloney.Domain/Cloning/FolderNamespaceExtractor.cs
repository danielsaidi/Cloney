using System;
using Cloney.Domain.Cloning.Abstractions;
using Cloney.Domain.Extensions;

namespace Cloney.Domain.Cloning
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
