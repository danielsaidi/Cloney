using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to work with file system files.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/nextra
    /// </remarks>
    public interface IFile
    {
        void AppendAllLines(string path, IEnumerable<string> lines);
        void AppendAllLines(string path, IEnumerable<string> lines, Encoding encoding);
        void AppendAllText(string path, string content);
        void AppendAllText(string path, string content, Encoding encoding);
        StreamWriter AppendText(string path);
        void Copy(string sourcePath, string destinationPath);
        void Copy(string sourcePath, string destinationPath, bool overwrite);
        FileStream Create(string path);
        FileStream Create(string path, int bufferSize);
        FileStream Create(string path, int bufferSize, FileOptions options);
        FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity);
        StreamWriter CreateText(string path);
        void Decrypt(string path);
        void Delete(string path);
        void Encrypt(string path);
        bool Exists(string path);
        FileSecurity GetAccessControl(string path);
        FileSecurity GetAccessControl(string path, AccessControlSections includeSections);
        FileAttributes GetAttributes(string path);
        DateTime GetCreationTime(string path);
        DateTime GetCreationTimeUtc(string path);
        DateTime GetLastAccessTime(string path);
        DateTime GetLastAccessTimeUtc(string path);
        DateTime GetLastWriteTime(string path);
        DateTime GetLastWriteTimeUtc(string path);
        void Move(string sourcePath, string destinationPath);
        FileStream Open(string path, FileMode mode);
        FileStream Open(string path, FileMode mode, FileAccess access);
        FileStream Open(string path, FileMode mode, FileAccess access, FileShare share);
        FileStream OpenRead(string path);
        StreamReader OpenText(string path);
        FileStream OpenWrite(string path);
        IEnumerable<byte> ReadAllBytes(string path);
        IEnumerable<string> ReadAllLines(string path);
        IEnumerable<string> ReadAllLines(string path, Encoding encoding);
        string ReadAllText(string path);
        string ReadAllText(string path, Encoding encoding);
        IEnumerable<string> ReadLines(string path);
        IEnumerable<string> ReadLines(string path, Encoding encoding);
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName);
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
        void SetAccessControl(string path, FileSecurity fileSecurity);
        void SetAttributes(string path, FileAttributes fileAttributes);
        void SetCreationTime(string path, DateTime creationTime);
        void SetCreationTimeUtc(string path, DateTime creationTimeUtc);
        void SetLastAccessTime(string path, DateTime lastAccessTime);
        void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);
        void SetLastWriteTime(string path, DateTime lastWriteTime);
        void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);
        void WriteAllBytes(string path, IEnumerable<byte> bytes);
        void WriteAllLines(string path, IEnumerable<string> lines);
        void WriteAllText(string path, string content);
    }
}