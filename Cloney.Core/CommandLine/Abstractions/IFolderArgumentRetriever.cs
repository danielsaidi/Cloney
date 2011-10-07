using System.Collections.Generic;

namespace Cloney.Core.CommandLine.Abstractions
{
    public interface IFolderArgumentRetriever
    {
        string GetFolderArgumentValue(IDictionary<string, string> applicationArguments, string folderArgumentName, string folderDisplayName, string errorMessagePattern);
    }
}