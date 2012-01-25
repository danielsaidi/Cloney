using System;
using Cloney.Core.Old.Abstractions;
using Cloney.Core.Old.Extensions;
using NExtra.Extensions;

namespace Cloney.Core.Old
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
