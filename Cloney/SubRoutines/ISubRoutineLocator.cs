using System.Collections.Generic;

namespace Cloney.SubRoutines
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to find sub routines.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface ISubRoutineLocator
    {
        IEnumerable<ISubRoutine> FindAll();
    }
}
