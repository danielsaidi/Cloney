using System.Collections.Generic;

namespace Cloney.Core
{
    /// <summary>
    /// This interface describes a program that use args
    /// to start.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface IProgram
    {
        void Start(IEnumerable<string> args);
    }
}