using System.Collections.Generic;

namespace Cloney.Domain.IO.Abstractions
{
    public interface ICanMatchPathPattern
    {
        bool IsMatch(string path, string pattern);
        bool IsAnyMatch(string path, IEnumerable<string> patterns);
    }
}
