using System;
using System.Collections.Generic;
using System.IO;
using Cloney.Core.Cloning.Abstractions;
using Cloney.Core.Extensions;

namespace Cloney.Core.Cloning
{
    /// <summary>
    /// This class can be used to clone a Visual Studio solution.
    /// </summary>
    /// <remarks>
    /// The class can be instructed to exclude certain files and
    /// folders. It can also be told to direct copy certain file
    /// types, instead of the default "deep copy", which creates
    /// new files and fills them with modified content.
    /// </remarks>
    public class OldSolutionCloner : ICanCloneSolution
    {
        public OldSolutionCloner(IEnumerable<string> excludeFolderNames, IEnumerable<string> excludeFileNames, IEnumerable<string> excludeFileTypes, IEnumerable<string> plainCopyFileTypes)
        {
            ExcludeFolderNames = excludeFolderNames;
            ExcludeFileNames = excludeFileNames;
            ExcludeFileTypes = excludeFileTypes;
            PlainCopyFileTypes = plainCopyFileTypes;
        }


        public string CurrentPath { get; private set; }

        private IEnumerable<string> ExcludeFolderNames { get; set; }

        private IEnumerable<string> ExcludeFileNames { get; set; }

        private IEnumerable<string> ExcludeFileTypes { get; set; }

        private IEnumerable<string> PlainCopyFileTypes { get; set; }
        
        private string SourceFolder { get; set; }
        
        private string SourceNamespace { get; set; }
        
        private string TargetFolder { get; set; }
        
        private string TargetNamespace { get; set; }


        public event EventHandler CloningBegun;

        public event EventHandler CloningEnded;


        private string AdjustPath(string path)
        {
            path = path.Replace(SourceFolder, "");
            path = ReplaceNamespace(path);

            return path;
        }

        public void CloneSolution(string sourcePath, string sourceNamespace, string targetPath, string targetNamespace)
        {
            OnCloningBegun(null);

            SourceFolder = sourcePath;
            SourceNamespace = sourceNamespace;
            TargetFolder = targetPath;
            TargetNamespace = targetNamespace;

            CopyFolders(SourceFolder);
            CopyFiles(SourceFolder);

            OnCloningEnded(null);

            CurrentPath = "";
        }

        private void CopyFiles(string folderPath)
        {
            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                CurrentPath = filePath;

                var fileInfo = new FileInfo(filePath);
                if (IsExcluded(fileInfo))
                    continue;

                var targetPath = TargetFolder + AdjustPath(filePath);

                if (IsPlainCopyFileType(fileInfo))
                {
                    File.Copy(filePath, targetPath, true);
                    continue;
                }

                var sourceStream = new StreamReader(filePath);
                var sourceContent = sourceStream.ReadToEnd();
                sourceStream.Close();

                if (!sourceContent.Contains(TargetNamespace))
                {
                    File.Copy(filePath, targetPath, true);
                    continue;
                }

                var targetStream = new StreamWriter(targetPath);
                targetStream.Write(ReplaceNamespace(sourceContent));
                targetStream.Close();
            }
        }

        private void CopyFolders(string parentFolderPath)
        {
            foreach (var directory in Directory.GetDirectories(parentFolderPath))
            {
                CurrentPath = directory;

                var dirInfo = new DirectoryInfo(directory);
                if (IsExcluded(dirInfo))
                    continue;

                var folderName = TargetFolder + AdjustPath(directory);
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);

                CopyFolders(directory);
                CopyFiles(directory);
            }
        }

        private bool IsExcluded(DirectoryInfo dirInfo)
        {
            return ExcludeFolderNames.Contains(dirInfo.Name, true);
        }

        private bool IsExcluded(FileInfo fileInfo)
        {
            return ExcludeFileNames.Contains(fileInfo.Name, true) || ExcludeFileTypes.Contains(fileInfo.Extension.Replace(".", ""), true);
        }

        private bool IsPlainCopyFileType(FileInfo fileInfo)
        {
            return PlainCopyFileTypes.Contains(fileInfo.Extension.Replace(".", ""), true);
        }

        private void OnCloningBegun(EventArgs e)
        {
            if (CloningBegun != null)
                CloningBegun(this, e);
        }

        private void OnCloningEnded(EventArgs e)
        {
            if (CloningEnded != null)
                CloningEnded(this, e);
        }

        private string ReplaceNamespace(string str)
        {
            str = str.Replace(SourceNamespace, TargetNamespace);
            str = str.Replace(SourceNamespace.ToLower(), TargetNamespace.ToLower());
            str = str.Replace(SourceNamespace.Replace(".", ""), TargetNamespace.Replace(".", ""));
            str = str.Replace(SourceNamespace.ToLower().Replace(".", ""), TargetNamespace.Replace(".", "").ToLower());

            return str;
        }
    }
}
