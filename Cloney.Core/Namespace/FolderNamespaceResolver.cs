﻿namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This class can be used to resolve the namespace
    /// of a solution based on the solution root folder.
    /// </summary>
    public class FolderNamespaceResolver : INamespaceResolver
    {
        public string ResolveNamespace(string folderPath)
        {
            folderPath = folderPath.Replace("/", "\\");
            folderPath = folderPath.EndsWith("\\") ? folderPath.TrimEnd('\\') : folderPath;

            var slashIndex = folderPath.LastIndexOf("\\") + 1;
            
            return folderPath.Substring(slashIndex, folderPath.Length - slashIndex);
        }
    }
}
