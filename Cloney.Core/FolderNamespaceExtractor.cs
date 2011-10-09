using System;
using Cloney.Core.Abstractions;
using Cloney.Core.Extensions;
using NExtra.Extensions;

namespace Cloney.Core
{
    public class FolderNamespaceExtractor : ICanExtractNamespace
    {
        public string ExtractNamespace(string folderPath)
        {
            if (folderPath.IsNullOrEmpty())
                return String.Empty;
            
            folderPath = folderPath.AdjustPathSlashes();

            var slashIndex = folderPath.LastIndexOf("\\") + 1;
            
            return folderPath.Substring(slashIndex, folderPath.Length - slashIndex);
        }
    }
}
