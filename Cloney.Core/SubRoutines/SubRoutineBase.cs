using System.Collections.Generic;

namespace Cloney.Core.SubRoutines
{
    public abstract class SubRoutineBase
    {
        public bool Finished { get; private set; }


        protected bool ArgHasKeyWithValue(Dictionary<string, string> args, string key, string value)
        {
            return (args.ContainsKey(key) && args[key] == value);
        }

        public void Finish()
        {
            Finished = true;
        }
    }
}
