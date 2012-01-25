using System.Collections.Generic;

namespace Cloney.Core
{
    public interface IProgram
    {
        void Start(IEnumerable<string> args);
    }
}