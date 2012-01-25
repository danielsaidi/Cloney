using Cloney.Core.Old.Abstractions;

namespace Cloney.Core.Old.CommandLine.Abstractions
{
    public interface IFolderNamespaceRetriever
    {
        string GetFolderNamespace(ICanExtractNamespace namespaceExtractor, string folderPath, string errorMessagePattern);
    }
}