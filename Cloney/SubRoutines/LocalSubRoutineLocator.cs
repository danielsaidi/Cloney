using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NExtra.Extensions;

namespace Cloney.SubRoutines
{
    /// <summary>
    /// This class can be used to find every sub routine
    /// that is defined in the Cloney library.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
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
