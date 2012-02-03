using System.Collections.Generic;
using Cloney.Core.SubRoutines;

namespace Cloney.Core
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
