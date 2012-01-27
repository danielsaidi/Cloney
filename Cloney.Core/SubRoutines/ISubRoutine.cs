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
    /// 
    /// When a routine finishes, it MUST set the Finished
    /// property to true. If a routine fails to do so, it
    /// will cause Cloney to loooop until the end of time.
    /// </remarks>
    public interface ISubRoutine
    {
        bool Finished { get; }

        void Run(IDictionary<string, string> args);
    }
}
