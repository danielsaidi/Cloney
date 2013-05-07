﻿using System.Collections.Generic;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This abstract base class can provide sub routines
    /// with basic functionality. Every sub routine needs
    /// a default constructor.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public abstract class SubRoutineBase
    {
        protected bool HasArg(IDictionary<string, string> args, string key, string value)
        {
            return (args.ContainsKey(key) && args[key] == value);
        }

        protected bool HasSingleArg(IDictionary<string, string> args, string key, string value)
        {
            return args.Count == 1 && HasArg(args, key, value);
        }
    }
}
