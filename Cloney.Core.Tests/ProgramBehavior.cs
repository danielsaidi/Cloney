﻿using System.Collections.Generic;
using Cloney.Core.Console;
using Cloney.Core.Localization;
using Cloney.Core.SubRoutines;
using NSubstitute;
using NUnit.Framework;

namespace Cloney.Core.Tests
{
    [TestFixture]
    public class ProgramBehavior
    {
        private Program program;

        private List<string> arguments;
        private IConsole console;
        private ITranslator translator;
        private ISubRoutineLocator subRoutineLocator;
 

        [SetUp]
        public void SetUp()
        {
            arguments = new List<string> { "foo", "bar" };
            console = Substitute.For<IConsole>();
            translator = Substitute.For<ITranslator>();
            translator.Translate(Arg.Any<string>()).Returns(x => x[0]);
            subRoutineLocator = Substitute.For<ISubRoutineLocator>();

            program = new Program(console, translator, subRoutineLocator);
        }


        [Test]
        public void Start_ShouldFailWithPrettyErrorMessage()
        {
            program = new Program(console, translator, null);

            program.Start(arguments);

            translator.Received().Translate("StartErrorMessage");
            console.Received().WriteLine("StartErrorMessage");
            console.Received().WriteLine("Object reference not set to an instance of an object.");
        }

        [Test]
        public void Start_ShouldLookForSubRoutines()
        {
            program.Start(arguments);

            subRoutineLocator.Received().FindAll();
        }

        [Test]
        public void Start_ShouldPrintNonMatchMessageForNoSubRoutines()
        {
            subRoutineLocator.FindAll().Returns(new List<ISubRoutine>());

            program.Start(arguments);

            translator.Received().Translate("NoRoutineMatchMessage");
            console.Received().WriteLine("NoRoutineMatchMessage");
        }

        [Test]
        public void Start_ShouldPrintNonMatchMessageForNonTriggeredSubRoutines()
        {
            var subRoutine = Substitute.For<ISubRoutine>();
            subRoutine.Run(Arg.Any<IEnumerable<string>>()).Returns(false);
            subRoutineLocator.FindAll().Returns(new List<ISubRoutine> { subRoutine });

            program.Start(arguments);

            translator.Received().Translate("NoRoutineMatchMessage");
            console.Received().WriteLine("NoRoutineMatchMessage");
        }

        [Test]
        public void Start_ShouldNotPrintNonMatchMessageForTriggeredSubRoutines()
        {
            var subRoutine = Substitute.For<ISubRoutine>();
            subRoutine.Run(Arg.Any<IEnumerable<string>>()).Returns(true);
            subRoutineLocator.FindAll().Returns(new List<ISubRoutine>{subRoutine});

            program.Start(arguments);

            console.DidNotReceive().WriteLine(Arg.Any<string>());
        }
    }
}