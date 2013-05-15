﻿using System;
using System.IO;
using Cloney.Core.IO;
using Cloney.Core.Namespace;

namespace Cloney.Core.Cloning
{
    /// <summary>
    /// This class can be used to clone Visual Studio solutions
    /// from a source to a target folder.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class SolutionCloner : SolutionClonerBase, ISolutionCloner
    {
        private readonly INamespaceResolver sourceNamespaceResolver;
        private readonly INamespaceResolver targetNamespaceResolver;
        private readonly ISolutionClonerBehavior cloningBehavior;
        private readonly IFileEncodingResolver fileEncodingResolver;


        public SolutionCloner(INamespaceResolver sourceNamespaceResolver, INamespaceResolver targetNamespaceResolver, ISolutionClonerBehavior cloningBehavior, IFileEncodingResolver fileEncodingResolver)
        {
            this.sourceNamespaceResolver = sourceNamespaceResolver;
            this.targetNamespaceResolver = targetNamespaceResolver;
            this.cloningBehavior = cloningBehavior;
            this.fileEncodingResolver = fileEncodingResolver;
        }


        public void CloneSolution(string solutionFilePath, string targetFolderPath)
        {
            OnCloningBegun(new EventArgs());

            var sourceNamespace = sourceNamespaceResolver.ResolveNamespace(solutionFilePath);
            var targetNamespace = targetNamespaceResolver.ResolveNamespace(targetFolderPath);
            var sourceFolderPath = new FileInfo(solutionFilePath).Directory;

            CloneSubFolders(sourceFolderPath, sourceFolderPath, sourceNamespace, targetFolderPath, targetNamespace);
            CloneFolderFiles(sourceFolderPath, sourceFolderPath, sourceNamespace, targetFolderPath, targetNamespace);
            CurrentPath = "";

            OnCloningEnded(new EventArgs());
        }


        private void CloneFolderFiles(DirectoryInfo sourceFolderPath, DirectoryInfo sourceRootPath, string sourceNamespace, string targetPath, string targetNamespace)
        {
            foreach (var filePath in Directory.GetFiles(sourceFolderPath.ToString()))
            {
                CurrentPath = filePath;

                var fileName = new FileInfo(filePath).Name;
                if (cloningBehavior.ShouldExcludeFile(fileName))
                    continue;

                var adjustedTargetPath = GetAdjustedTargetPath(filePath, sourceRootPath, sourceNamespace, targetNamespace);
                var targetFilePath = Path.Combine(targetPath, adjustedTargetPath);

                if (cloningBehavior.ShouldPlainCopyFile(fileName))
                {
                    File.Copy(filePath, targetFilePath, true);
                    continue;
                }

                //Detect the encoding and then apply it again when writing the new file
                var encoding = fileEncodingResolver.ResolveFileEncoding(filePath);
                var sourceStream = new StreamReader(filePath, encoding);
                var sourceContent = sourceStream.ReadToEnd();

                //StreamReader can possibly use a different encoding than the one we provide; 
                //but we want to write back with the same encoding that we used to read...
                var sourceEncoding = sourceStream.CurrentEncoding;
                sourceStream.Close();

                if (!sourceContent.Contains(sourceNamespace))
                {
                    File.Copy(filePath, targetFilePath, true);
                    continue;
                }

                var targetStream = new StreamWriter(new FileStream(targetFilePath, FileMode.CreateNew), sourceEncoding);
                targetStream.Write(ReplaceNamespace(sourceContent, sourceNamespace, targetNamespace));
                targetStream.Close();
            }
        }

        private void CloneSubFolders(DirectoryInfo sourceFolderPath, DirectoryInfo sourceRootPath, string sourceNamespace, string targetPath, string targetNamespace)
        {
            foreach (var directory in Directory.GetDirectories(sourceFolderPath.ToString()))
            {
                CurrentPath = directory;
                var folderInfo = new DirectoryInfo(directory);
                var folderName = folderInfo.Name;
                if (cloningBehavior.ShouldExcludeFolder(folderName))
                    continue;

                var adjustedTargetPath = GetAdjustedTargetPath(directory, sourceRootPath, sourceNamespace, targetNamespace);
                var targetFolderPath = Path.Combine(targetPath, adjustedTargetPath);

                if (!Directory.Exists(targetFolderPath))
                    Directory.CreateDirectory(targetFolderPath);

                CloneSubFolders(folderInfo, sourceRootPath, sourceNamespace, targetPath, targetNamespace);
                CloneFolderFiles(folderInfo, sourceRootPath, sourceNamespace, targetPath, targetNamespace);
            }
        }

        private static string GetAdjustedTargetPath(string filePath, FileSystemInfo sourceRootPath, string sourceNamespace, string targetNamespace)
        {
            var adjustedSourceRoot = ReplaceNamespace(sourceRootPath.FullName, sourceNamespace, targetNamespace);
            var adjustedTargetPath = ReplaceNamespace(filePath, sourceNamespace, targetNamespace);
            adjustedTargetPath = adjustedTargetPath.Replace(adjustedSourceRoot, "");
            if (adjustedTargetPath.StartsWith("\\"))
                adjustedTargetPath = adjustedTargetPath.Substring(1);
            return adjustedTargetPath;
        }

        private static string ReplaceNamespace(string str, string sourceNamespace, string targetNamespace)
        {
            str = str.Replace(sourceNamespace, targetNamespace);
            str = str.Replace(sourceNamespace.ToLower(), targetNamespace.ToLower());
            return str;
        }
    }
}
