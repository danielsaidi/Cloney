using System.Collections.Generic;

namespace Cloney.SubRoutines
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be called by the Cloney console program to do
    /// something according to input arguments.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// 
    /// Cloney ISubRoutines must (for now) have a default
    /// parameterless constructor, since they are created
    /// with reflection.
    /// </remarks>
    public interface ISubRoutine
    {
        bool Run(IDictionary<string, string> args);
    }
}
