using System.Linq;
using NUnit.Framework;

namespace Cloney.Core.Tests
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

            Assert.That(routines.Count(), Is.EqualTo(7));
        }

        [Test]
        [TestCase("CloneRoutine")]
        [TestCase("GuiRoutine")]
        [TestCase("HelpRoutine")]
        [TestCase("InstallContextMenuRoutine")]
        [TestCase("ModalGuiRoutine")]
        [TestCase("SettingsRoutine")]
        [TestCase("UninstallContextMenuRoutine")]
        public void FindAll_ShouldFindSubRoutineInCoreLibrary(string routineName)
        {
            var routines = locator.FindAll().ToList();
            var routineNames = routines.Select(x => x.GetType().Name);

            Assert.That(routineNames.Contains(routineName), Is.True);
        }
    }
}