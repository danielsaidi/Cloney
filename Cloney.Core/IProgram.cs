using System.Collections.Generic;

namespace Cloney.Core
{
    public interface IProgram
    {
        bool Start(IEnumerable<string> args);
    }
}