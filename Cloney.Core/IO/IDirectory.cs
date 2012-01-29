using System.Collections.Generic;
using System.IO;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This interface can be implemented by classes
    /// that can be used to handle directory-related
    /// file system operations.
    /// </summary>
    public interface IDirectory
    {
        DirectoryInfo CreateDirectory(string path);
        bool Exists(string path);
        IEnumerable<string> GetFiles(string path);
    }
}
