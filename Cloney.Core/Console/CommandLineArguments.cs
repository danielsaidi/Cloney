using System.Collections.Generic;

namespace Cloney.Core.Console
{
    public class CommandLineArguments
    {
        private readonly IDictionary<string, string> args;


        public CommandLineArguments(IDictionary<string, string> args)
        {
            this.args = args;
        }


        public IDictionary<string, string> Raw
        {
            get { return args; }
        }


        public bool HasArgument(string key)
        {
            return (args.ContainsKey(key));
        }

        public bool HasArgument(string key, string value)
        {
            return (args.ContainsKey(key) && args[key] == value);
        }

        public bool HasSingleArgument(string key)
        {
            return args.Count == 1 && HasArgument(key);
        }

        public bool HasSingleArgument(string key, string value)
        {
            return args.Count == 1 && HasArgument(key, value);
        }
    }
}
