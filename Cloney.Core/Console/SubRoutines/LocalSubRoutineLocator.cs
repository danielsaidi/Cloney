using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NExtra.Extensions;

namespace Cloney.Core.Console.SubRoutines
{
    /// <summary>
    /// This class can be used to find all Cloney
    /// sub routines that are defined in the Core
    /// namespace.
    /// </summary>
    public class LocalSubRoutineLocator : ISubRoutineLocator
    {
        public IEnumerable<ISubRoutine> FindAll()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var routines = assembly.GetTypesThatImplement(typeof (ISubRoutine));

            return routines.Select(routine => (ISubRoutine) Activator.CreateInstance(routine)).ToList();
        }
    }
}
