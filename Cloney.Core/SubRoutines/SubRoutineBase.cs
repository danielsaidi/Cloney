using System.Collections.Generic;

namespace Cloney.Core.SubRoutines
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
        protected bool HasArg(IDictionary<string, string> args, string key)
        {
            return (args.ContainsKey(key));
        }

        protected bool HasArg(IDictionary<string, string> args, string key, string value)
        {
            return (HasArg(args, key) && args[key] == value);
        }

        protected bool HasSingleArg(IDictionary<string, string> args, string key, string value)
        {
            return args.Count == 1 && HasArg(args, key, value);
        }
    }
}
