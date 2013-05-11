using System.Collections.Generic;
using Cloney.Core.Cloning;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests.SubRoutines
{
    [TestFixture]
    public class CloneRoutineBehavior : SubRoutineTestBase
    {
        private ISubRoutine routine;
        private ISolutionCloner solutionCloner;

        private readonly Dictionary<string, string> missingSourceArgs = new Dictionary<string, string> {{"clone", "true"},{"source", ""},{"target", "bar"}};
        private readonly Dictionary<string, string> missingTargetArgs = new Dictionary<string, string> {{"clone", "true"},{"source", "foo"},{"target", ""}};
        private readonly Dictionary<string, string> validArgs = new Dictionary<string, string> {{"clone", "true"},{"source", "foo"},{"target", "bar"}};
        

        [SetUp]
        public void SetUp()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(validArgs));

            solutionCloner = Substitute.For<ISolutionCloner>();
            routine = new CloneRoutine(solutionCloner, ArgumentParser, Console, Translator);
        }


        [Test]
        public void Run_ShouldParseIEnumerableToDictionary()
        {
            routine.Run(args);

            ArgumentParser.Received().ParseCommandLineArguments(args);
        }

        [Test]
        public void Run_ShouldAbortForNoArguments()
        {
            var emptyArgs = new Dictionary<string, string>();
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(emptyArgs));
            var result = routine.Run(args);

            AssertSubRoutineStopped(result);
        }
        
        [Test]
        public void Run_ShouldAbortForIrrelevantArguments()
        {
            var irrelevantArgs = new Dictionary<string, string> {{"foo", "true"}, {"bar", "true"}};
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(irrelevantArgs));
            var result = routine.Run(args);

            AssertSubRoutineStopped(result);
        }

        [Test]
        public void Run_ShouldProceedForMissingSourcePath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingSourceArgs));
            var result = routine.Run(args);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldInstructAboutConsoleInputForMissingSourcePath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingSourceArgs));
            routine.Run(args);

            Translator.Received().Translate("EnterFolderPath");
            Console.Received().Write("EnterFolderPath");
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingSourcePath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingSourceArgs));
            routine.Run(args);

            Console.Received().ReadLine();
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingSourcePath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingSourceArgs));
            Console.ReadLine().Returns("source path");
            routine.Run(args);

            Console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("source path", "bar");
        }

        [Test]
        public void Run_ShouldProceedForMissingTargetPath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingTargetArgs));
            var result = routine.Run(args);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldInstructAboutConsoleInputForMissingTargetPath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingTargetArgs));
            routine.Run(args);

            Translator.Received().Translate("EnterFolderPath");
            Console.Received().Write("EnterFolderPath");
        }

        [Test]
        public void Run_ShouldRequireConsoleInputForMissingTargetPath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingTargetArgs));
            routine.Run(args);

            Console.Received().ReadLine();
        }

        [Test]
        public void Run_ShouldCloneWithConsoleInputForMissingTargetPath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(missingTargetArgs));
            Console.ReadLine().Returns("target path");
            routine.Run(args);

            Console.Received().ReadLine();
            solutionCloner.Received().CloneSolution("foo", "target path");
        }

        [Test]
        public void Run_ShouldProceedForProvidedSourceAndTargetPaths()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(validArgs));
            var result = routine.Run(args);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Run_ShouldNotInstructAboutConsoleInputForProvidedSourceAndTargetPath()
        {
            ArgumentParser.ParseCommandLineArguments(args).Returns(GetArgs(validArgs));
            routine.Run(args);

            Translator.DidNotReceive().Translate(Arg.Any<string>());
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
        }

        [Test]
        public void Run_ShouldNotRequireConsoleInputForProvidedSourceAndTargetPath()
        {
            routine.Run(args);

            Console.DidNotReceive().ReadLine();
        }

        [Test]
        public void Run_ShouldCloneWithProvidedSourceAndTargetPaths()
        {
            routine.Run(args);

            solutionCloner.Received().CloneSolution("foo", "bar");
        }

        [Test]
        public void Run_ShouldWriteToConsoleWhenCurrentPathChanges()
        {
            var cloner = new SolutionCloner(null, null, null);
            routine = new CloneRoutine(cloner, ArgumentParser, Console, Translator);

            cloner.CurrentPath = "apa";

            Console.Received().WriteLine("apa");
        }


        private void AssertSubRoutineStopped(bool result)
        {
            Assert.That(result, Is.False);
            Translator.DidNotReceive().Translate(Arg.Any<string>());
            Console.DidNotReceive().WriteLine(Arg.Any<string>());
            solutionCloner.DidNotReceive().CloneSolution(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}