using System.Collections.Generic;
using NExtra;

namespace Cloney.Core.SubRoutines
{
    /// <summary>
    /// This sub routine will trigger on the --help
    /// argument and print information about how to
    /// use Cloney.
    /// </summary>
    public class HelpRoutine : ISubRoutine
    {
        public HelpRoutine()
            :this(new ConsoleFacade())
        {
        }

        public HelpRoutine(IConsole console)
        {   
        }


        public void Run(IDictionary<string, string> args)
        {
            if (args.Keys.Contains("help"))
                System.Console.WriteLine("Skriv ut hjälp");
        }
    }
}
