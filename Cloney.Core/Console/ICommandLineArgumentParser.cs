using System.Collections.Generic;

namespace Cloney.Core.Console
{
    ///<summary>
    /// This interface can be implemented by classes that
    /// can parse command line arguments.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/nextra
    /// </remarks>
    public interface ICommandLineArgumentParser
    {
        CommandLineArguments ParseCommandLineArguments(IEnumerable<string> args);
    }
}
