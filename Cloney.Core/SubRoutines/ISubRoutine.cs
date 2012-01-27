using System.Collections.Generic;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be called by the Cloney console program to do
    /// something according to input arguments.
    /// </summary>
    /// <remarks>
    /// Cloney ISubRoutines must (for now) have a default
    /// parameterless constructor, since they are created
    /// with reflection.
    /// </remarks>
    public interface ISubRoutine
    {
        void Run(IDictionary<string, string> args);
    }
}
