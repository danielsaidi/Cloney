using System.Collections.Generic;

namespace Cloney.Core.IO
{
    /// <summary>
    /// This interface can be implemented by classes
    /// that can be used to match file and directory
    /// paths with a pattern (e.g. *.txt, b*, a*.txt).
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// </remarks>
    public interface IPathPatternMatcher
    {
        bool IsAnyMatch(string path, IEnumerable<string> patterns);
        bool IsMatch(string path, string pattern);
    }
}
