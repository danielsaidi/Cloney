﻿using System.Linq;
using Cloney.Core.SubRoutines;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class LocalSubRoutineLocatorBehavior
    {
        private ISubRoutineLocator locator;


        [SetUp]
        public void SetUp()
        {
            locator = new LocalSubRoutineLocator();
        }


        [Test]
        public void FindAll_ShouldFindAllSubRoutinesInCoreLibrary()
        {
            var routines = locator.FindAll().ToList();
            var routineNames = routines.Select(x => x.GetType().Name);

            Assert.That(routines.Count(), Is.EqualTo(5));
            Assert.That(routineNames.Contains("CloneRoutine"), Is.True);
            Assert.That(routineNames.Contains("HelpRoutine"), Is.True);
            Assert.That(routineNames.Contains("GuiRoutine"), Is.True);
            Assert.That(routineNames.Contains("SettingsRoutine"), Is.True);
            Assert.That(routineNames.Contains("InstallContextMenuRoutine"), Is.True);
        }
    }
}