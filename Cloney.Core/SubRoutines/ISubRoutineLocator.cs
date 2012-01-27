using System.Collections.Generic;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to find Cloney sub routines.
    /// </summary>
    public interface ISubRoutineLocator
    {
        IEnumerable<ISubRoutine> FindAll();
    }
}
