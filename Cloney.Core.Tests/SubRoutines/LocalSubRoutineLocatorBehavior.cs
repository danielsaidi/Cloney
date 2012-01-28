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

            Assert.That(routines.Count(), Is.EqualTo(3));
            Assert.That(routineNames.Contains("CloneRoutine"), Is.True);
            Assert.That(routineNames.Contains("GeneralHelpRoutine"), Is.True);
            Assert.That(routineNames.Contains("GuiApplicationRoutine"), Is.True);
        }
    }
}