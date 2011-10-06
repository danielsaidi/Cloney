using System;
using System.Collections.Specialized;
using Cloney.Core.CommandLine;
using Cloney.Core.CommandLine.Abstractions;
using NUnit.Framework;

namespace Cloney.Core.Tests.CommandLine
{
    [TestFixture]
    public class RequiredArgumentValidatorBehavior
    {
        private IArgumentValidator obj;


        [SetUp]
        public void SetUp()
        {
            obj = new RequiredArgumentValidator();
        }


        [Test, ExpectedException(typeof(NullReferenceException))]
        public void Validate_ShouldFailForMissingArgument()
        {
            obj.Validate(new StringDictionary(), "foo", "bar");
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void Validate_ShouldFailForNullArgument()
        {
            obj.Validate(new StringDictionary { { "foo", null } }, "foo", "bar");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Validate_ShouldFailForEmptyArgument()
        {
            obj.Validate(new StringDictionary { { "foo", "  " } }, "foo", "bar");
        }

        [Test]
        public void Validate_ShouldSucceedForExistingNonEmptyArgument()
        {
            obj.Validate(new StringDictionary { { "foo", "bar" } }, "foo", "bar");
        }
    }
}