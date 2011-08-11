using System.Collections.Specialized;

namespace Cloney.Core.Abstractions
{
    public interface ISettings
    {
        StringCollection ExcludeFilePatterns { get; }
        StringCollection ExcludeFolderPatterns { get; }
        StringCollection PlainCopyFilePatterns { get; }
    }
}
