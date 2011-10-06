using System;
using Cloney.Core.CommandLine.Abstractions;
using NExtra.Extensions;

namespace Cloney.Core.CommandLine
{
    public class RequiredNamespaceArgumentValidator : INamespaceArgumentValidator
    {
        public void Validate(string value, string errorMessage)
        {
            if (value.Trim().IsNullOrEmpty())
                throw new ArgumentException(errorMessage);
        }
    }
}
