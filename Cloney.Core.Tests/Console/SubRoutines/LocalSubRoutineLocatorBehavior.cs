using System.Linq;
using Cloney.Core.Console.SubRoutines;
using NUnit.Framework;

namespace Cloney.Core.Tests.Console.SubRoutines
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

            Assert.That(routines.Count(), Is.EqualTo(1));
            Assert.That(routines[0].GetType().Name, Is.EqualTo("HelpRoutine"));
        }
    }
}