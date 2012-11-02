using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cloney.Core.IO;
using Cloney.Core.Namespace;

namespace Cloney.Core.Cloners
{
    /// <summary>
    /// This class can be used to clone Visual Studio solutions
    /// from a source to a target folder.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    ///
    /// TODO: This was plain copied from Cloney 0.5, since this
    /// way to clone works. However, it should be rewritten and
    /// tested thoroughly.
    /// </remarks>
    public class SolutionCloner : SolutionClonerBase, ISolutionCloner
    {
        private readonly INamespaceResolver targetNamespaceResolver;
        private readonly INamespaceResolver sourceNamespaceResolver;
        private readonly IPathPatternMatcher pathPatternMatcher;
        private readonly IEnumerable<string> excludeFolderPatterns;
        private readonly IEnumerable<string> excludeFilePatterns;
        private readonly IEnumerable<string> plainCopyFilePatterns;


        public SolutionCloner(INamespaceResolver sourceNamespaceResolver, INamespaceResolver targetNamespaceResolver, IPathPatternMatcher pathPatternMatcher, IEnumerable<string> excludeFolderPatterns, IEnumerable<string> excludeFilePatterns, IEnumerable<string> plainCopyFilePatterns)
        {
            this.targetNamespaceResolver = targetNamespaceResolver;
            this.sourceNamespaceResolver = sourceNamespaceResolver;
            this.pathPatternMatcher = pathPatternMatcher;

            this.excludeFolderPatterns = excludeFolderPatterns;
            this.excludeFilePatterns = excludeFilePatterns;
            this.plainCopyFilePatterns = plainCopyFilePatterns;
        }


        public string AdjustPath(string path, string sourcePath, string sourceNamespace, string targetNamespace)
        {
            path = path.Replace(sourcePath, "");

            return ReplaceNamespace(path, sourceNamespace, targetNamespace);
        }

        public void CloneSolution(string sourcePath, string targetPath)
        {
            OnCloningBegun(new EventArgs());

            var sourceNamespace = sourceNamespaceResolver.ResolveNamespace(sourcePath);
            var targetNamespace = targetNamespaceResolver.ResolveNamespace(targetPath);

            CloneSubFolders(sourcePath, sourcePath, sourceNamespace, targetPath, targetNamespace);
            CloneFolderFiles(sourcePath, sourcePath, sourceNamespace, targetPath, targetNamespace);
            CurrentPath = "";

            OnCloningEnded(new EventArgs());
        }

        private void CloneFolderFiles(string folderPath, string sourcePath, string sourceNamespace, string targetPath, string targetNamespace)
        {
            if (string.IsNullOrEmpty(folderPath))
                return;

            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                CurrentPath = filePath;

                var fileName = new FileInfo(filePath).Name;
                if (IsExcludedFile(fileName))
                    continue;

                var adjustedFilePath = AdjustPath(filePath, sourcePath, sourceNamespace, targetNamespace);
                var targetFilePath = targetPath + adjustedFilePath;

                if (IsPlainCopyFile(fileName))
                {
                    File.Copy(filePath, targetFilePath, true);
                    continue;
                }

                // fixing issue #4 by trying to detect the encoding and then apply it again when writing the new file
                // StreamReader would fall back to UTF8 anyway...
                var encoding = KlerksSoftEncodingDetector.DetectTextFileEncoding(filePath, null) ?? Encoding.UTF8;
                var sourceStream = new StreamReader(filePath, encoding);
                var sourceContent = sourceStream.ReadToEnd();

                // StreamReader can possibly use a different encoding than the one we provide; 
                // but we want to write back with the same encoding that we used to read...
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

        private void CloneSubFolders(string parentFolderPath, string sourcePath, string sourceNamespace, string targetPath, string targetNamespace)
        {
            if (string.IsNullOrEmpty(parentFolderPath))
                return;

            foreach (var directory in Directory.GetDirectories(parentFolderPath))
            {
                CurrentPath = directory;

                var folderName = new DirectoryInfo(directory).Name;
                if (IsExcludedFolder(folderName))
                    continue;

                var adjustedFolderPath = AdjustPath(directory, sourcePath, sourceNamespace, targetNamespace);
                var targetFolderPath = targetPath + adjustedFolderPath;

                if (!Directory.Exists(targetFolderPath))
                    Directory.CreateDirectory(targetFolderPath);

                CloneSubFolders(directory, sourcePath, sourceNamespace, targetPath, targetNamespace);
                CloneFolderFiles(directory, sourcePath, sourceNamespace, targetPath, targetNamespace);
            }
        }


        public bool IsExcludedFolder(string folderPath)
        {
            return pathPatternMatcher.IsAnyMatch(folderPath, excludeFolderPatterns);
        }

        public bool IsExcludedFile(string filePath)
        {
            return pathPatternMatcher.IsAnyMatch(filePath, excludeFilePatterns);
        }

        public bool IsPlainCopyFile(string filePath)
        {
            return pathPatternMatcher.IsAnyMatch(filePath, plainCopyFilePatterns);
        }

        public string ReplaceNamespace(string str, string sourceNamespace, string targetNamespace)
        {
            str = str.Replace(sourceNamespace, targetNamespace);
            str = str.Replace(sourceNamespace.ToLower(), targetNamespace.ToLower());
            return str;
        }
    }
}
