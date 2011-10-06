using System.Collections.Specialized;
using Cloney.Core.Properties;

namespace Cloney.Core
{
    /// <summary>
    /// For now, this class only exposes the embedded application
    /// settings of the core library. If the various applications
    /// should be able to modify the settings, this class will be
    /// removed in favor of a separate app.config section in each
    /// application.
    /// </summary>
    public class CoreSettings
    {
        public static StringCollection ExcludeFilePatterns
        {
            get { return Settings.Default.ExcludeFilePatterns; }
        }

        public static StringCollection ExcludeFolderPatterns
        {
            get { return Settings.Default.ExcludeFolderPatterns; }
        }

        public static StringCollection PlainCopyFilePatterns
        {
            get { return Settings.Default.PlainCopyFilePatterns; }
        }
    }
}
