using System.Collections.Generic;

namespace Cloney.Core.Console
{
    ///<summary>
    /// This interface can be implemented by classes that
    /// can parse command line args to a sting dictionary.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// </remarks>
    public interface IArgumentParser<out T>
    {
        T ParseArguments(IEnumerable<string> args);
    }
}
