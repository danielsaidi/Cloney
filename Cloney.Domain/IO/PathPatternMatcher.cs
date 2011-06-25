using System;
using System.Collections.Generic;
using System.Linq;
using Cloney.Domain.IO.Abstractions;

namespace Cloney.Domain.IO
{
    public class PathPatternMatcher : ICanMatchPathPattern
    {
        public bool IsMatch(string path, string pattern)
        {
            return NativePatternMatcher.StrictMatchPattern(path, pattern);
        }

        public bool IsAnyMatch(string path, IEnumerable<string> patterns)
        {
            return patterns.Any(pattern => IsMatch(path, pattern));
        }
    }
}
