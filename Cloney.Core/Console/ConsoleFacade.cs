using System;

namespace Cloney.Core.Console
{
    /// <summary>
    /// This class can be used as a facade for the static
    /// Console class.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/nextra
    /// </remarks>
    public class ConsoleFacade : IConsole
    {
        public int Read()
        {
            return System.Console.Read();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public void Write(string value)
        {
            System.Console.Write(value);
        }

        public void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}
