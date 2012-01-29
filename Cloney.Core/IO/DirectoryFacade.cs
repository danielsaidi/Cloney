using System.Collections.Generic;
using System.IO;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This class can be used as a facade for the
    /// static Directory class.
    /// </summary>
    public class DirectoryFacade : IDirectory
    {
        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}
