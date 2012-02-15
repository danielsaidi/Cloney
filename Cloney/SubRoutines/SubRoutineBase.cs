using System.Collections.Generic;

namespace Cloney.SubRoutines
{
    /// <summary>
    /// This abstract base class can provide sub routines
    /// with basic functionality.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public abstract class SubRoutineBase
    {
        protected bool ArgsHaveKeyValue(IDictionary<string, string> args, string key, string value)
        {
            return (args.ContainsKey(key) && args[key] == value);
        }

        protected bool ArgsHaveSingleKeyValue(IDictionary<string, string> args, string key, string value)
        {
            return args.Count == 1 && ArgsHaveKeyValue(args, key, value);
        }
    }
}
