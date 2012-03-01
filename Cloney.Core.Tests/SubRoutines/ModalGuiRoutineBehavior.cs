using Cloney.Core.Diagnostics;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class ModalGuiRoutineBehavior
    {
        private ISubRoutine routine;
        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            process = Substitute.For<IProcess>();

            routine = new ModalGuiRoutine(process);
        }


        [Test]
        public void Start_ShouldReturnFalseForNoArguments()
        {
            var result = routine.Run(new string[] { });

            Assert.That(result, Is.False);
        }

        [Test]
        public void Start_ShouldAbortForNoArguments()
        {
            routine.Run(new string[] { });

            process.DidNotReceive().Start(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Start_ShouldReturnTrueForSingleModalArgument()
        {
            var result = routine.Run(new[] { "--modal" });

            Assert.That(result, Is.True);
        }

        [Test]
        public void Start_ShouldLaunchExternalProgramForSingleModalArgument()
        {
            routine.Run(new[] { "--modal" });

            process.Received().Start("Cloney.Wizard.exe", "--modal");
        }

        [Test]
        public void Start_ShouldReturnTrueForNonSingleModalArgument()
        {
            var result = routine.Run(new[] { "--modal", "--foo" });

            Assert.That(result, Is.True);
        }

        [Test]
        public void Start_ShouldLaunchExternalProgramForNonSingleModalArgument()
        {
            routine.Run(new[] { "--modal", "--foo" });

            process.Received().Start("Cloney.Wizard.exe", "--modal");
        }

        [Test]
        public void Start_ShouldReturnTrueForModalAndSourceArguments()
        {
            var result = routine.Run(new[] { "--modal", "--source=foo" });

            Assert.That(result, Is.True);
        }

        [Test]
        public void Start_ShouldLaunchExternalProgramForModalAndSourceArguments()
        {
            routine.Run(new[] { "--modal", "--source=foo" });

            process.Received().Start("Cloney.Wizard.exe", "--modal --source=foo");
        }
    }
}