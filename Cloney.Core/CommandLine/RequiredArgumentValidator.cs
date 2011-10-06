using System;
using System.Collections.Specialized;
using Cloney.Core.CommandLine.Abstractions;
using NExtra.Extensions;

namespace Cloney.Core.CommandLine
{
    public class RequiredArgumentValidator : IArgumentValidator
    {
        public void Validate(StringDictionary arguments, string argumentName, string errorMessage)
        {
            if (arguments[argumentName].Trim().IsNullOrEmpty())
                throw new ArgumentException(errorMessage);
        }
    }
}
