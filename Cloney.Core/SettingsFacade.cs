using System.Collections.Specialized;
using Cloney.Core.Abstractions;
using Cloney.Core.Properties;

namespace Cloney.Core
{
    public class SettingsFacade : ISettings
    {
        public StringCollection ExcludeFilePatterns
        {
            get { return Settings.Default.ExcludeFilePatterns; }
        }

        public StringCollection ExcludeFolderPatterns
        {
            get { return Settings.Default.ExcludeFolderPatterns; }
        }

        public StringCollection PlainCopyFilePatterns
        {
            get { return Settings.Default.PlainCopyFilePatterns; }
        }
    }
}
