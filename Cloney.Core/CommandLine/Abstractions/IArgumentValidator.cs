using System.Collections.Specialized;

namespace Cloney.Core.CommandLine.Abstractions
{
    public interface IArgumentValidator
    {
        void Validate(StringDictionary arguments, string argumentName, string errorMessage);
    }
}