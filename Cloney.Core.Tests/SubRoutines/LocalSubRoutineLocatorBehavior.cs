using System.Linq;
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

            var count = 0;

            Assert.That(routineNames.Contains("CloneRoutine"), Is.True); count++;
            Assert.That(routineNames.Contains("HelpRoutine"), Is.True); count++;
            Assert.That(routineNames.Contains("GuiRoutine"), Is.True); count++;
            Assert.That(routineNames.Contains("SettingsRoutine"), Is.True); count++;
            Assert.That(routineNames.Contains("InstallContextMenuRoutine"), Is.True); count++;

            Assert.That(routines.Count(), Is.EqualTo(count));
        }
    }
}