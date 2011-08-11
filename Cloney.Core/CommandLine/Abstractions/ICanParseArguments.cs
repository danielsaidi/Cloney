using System.Collections.Generic;
using System.Collections.Specialized;

namespace Cloney.Core.CommandLine.Abstractions
{
    interface ICanParseArguments
    {
        StringDictionary ParseArguments(IEnumerable<string> args);
    }
}
