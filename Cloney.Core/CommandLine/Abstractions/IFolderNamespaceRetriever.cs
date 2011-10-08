using Cloney.Core.Abstractions;

namespace Cloney.Core.CommandLine.Abstractions
{
    public interface IFolderNamespaceRetriever
    {
        string GetFolderNamespace(ICanExtractNamespace namespaceExtractor, string folderPath, string errorMessagePattern);
    }
}