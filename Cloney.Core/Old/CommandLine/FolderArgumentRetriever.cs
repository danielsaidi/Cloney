using System;
using System.Collections.Generic;
using Cloney.Core.Old.CommandLine.Abstractions;
using NExtra.Extensions;

namespace Cloney.Core.Old.CommandLine
{
    //TODO:Test
    public class FolderArgumentRetriever : IFolderArgumentRetriever
    {
        public string GetFolderArgumentValue(IDictionary<string, string> applicationArguments, string folderArgumentName, string folderDisplayName, string errorMessagePattern)
        {
            string result;

            try
            {
                result = applicationArguments.Get(folderArgumentName).Trim();
                if (result.IsNullOrEmpty())
                    throw new Exception();
            }
            catch
            {
                var errorMessage = String.Format(errorMessagePattern, folderArgumentName, folderDisplayName);
                throw new ArgumentException(errorMessage);
            }

            return result.Replace("/", "\\");
        }
    }
}
