using System.Collections.Generic;

namespace Cloney.Core.Console
{
    ///<summary>
    /// This interface can be implemented by classes
    /// that can be used to handle command line args.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// </remarks>
    public interface ICommandLineArgumentParser
    {
        /// <summary>
        /// Parse a collection of command line arguments.
        /// </summary>
        IDictionary<string, string> ParseCommandLineArguments(IEnumerable<string> args);
    }
}
