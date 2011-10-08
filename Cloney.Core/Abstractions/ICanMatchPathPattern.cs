using System.Collections.Generic;

namespace Cloney.Core.Abstractions
{
    public interface ICanMatchPathPattern
    {
        bool IsMatch(string path, string pattern);
        bool IsAnyMatch(string path, IEnumerable<string> patterns);
    }
}
