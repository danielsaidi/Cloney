using System.Collections.Generic;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This interface can be implemented by classes
    /// that can be used to match file and directory
    /// paths with patterns (e.g. *.txt, bi*).
    /// </summary>
    public interface IPathPatternMatcher
    {
        bool IsAnyMatch(string path, IEnumerable<string> patterns);
        bool IsMatch(string path, string pattern);
    }
}
