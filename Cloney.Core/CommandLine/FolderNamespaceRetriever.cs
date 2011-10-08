using System;
using Cloney.Core.Abstractions;
using Cloney.Core.CommandLine.Abstractions;
using NExtra.Extensions;

namespace Cloney.Core.CommandLine
{
    //TODO:Test
    public class FolderNamespaceRetriever : IFolderNamespaceRetriever
    {
        public string GetFolderNamespace(ICanExtractNamespace namespaceExtractor, string folderPath, string errorMessagePattern)
        {
            var @namespace = namespaceExtractor.ExtractNamespace(folderPath);

            try
            {
                if (@namespace.Trim().IsNullOrEmpty())
                    throw new Exception();
            }
            catch
            {
                var errorMessage = String.Format(errorMessagePattern, folderPath);
                throw new ArgumentException(errorMessage);
            }

            return @namespace;
        }
    }
}
