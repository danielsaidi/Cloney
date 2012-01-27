using System.Collections.Generic;
using Cloney.Core.SubRoutines;
using NExtra.Diagnostics;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class ProgramBehavior
    {
        private ISubRoutine routine;
        private IProcess process;


        [SetUp]
        public void SetUp()
        {
            process = Substitute.For<IProcess>();

            routine = new GuiRoutine(process);
        }


        [Test]
        public void Start_ShouldLaunchExternalProgramForNoArguments()
        {
            var args = new Dictionary<string, string>();

            routine.Run(args);

            process.Received().Start("Cloney.Wizard.exe");
        }

        [Test]
        public void Start_ShouldNotLaunchExternalProgramForArguments()
        {
            var args = new Dictionary<string, string> {{"foo", "bar"}};
            
            routine.Run(args);

            process.DidNotReceive().Start(Arg.Any<string>());
        }
    }
}