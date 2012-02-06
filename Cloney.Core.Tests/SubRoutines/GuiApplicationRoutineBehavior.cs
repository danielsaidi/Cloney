using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NExtra.Diagnostics;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class GuiApplicationRoutineBehavior
    {
        private ISubRoutine routine;
        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            process = Substitute.For<IProcess>();

            routine = new GuiApplicationRoutine(process);
        }


        [Test]
        public void Start_ShouldLaunchExternalProgramForNoArguments()
        {
            var args = new Dictionary<string, string>();

            var result = routine.Run(args);

            Assert.That(result, Is.True);
            process.Received().Start("Cloney.Wizard.exe");
        }

        [Test]
        public void Start_ShouldNotLaunchExternalProgramForArguments()
        {
            var args = new Dictionary<string, string> {{"foo", "bar"}};
            
            var result = routine.Run(args);

            Assert.That(result, Is.False);
            process.DidNotReceive().Start(Arg.Any<string>());
        }
    }
}