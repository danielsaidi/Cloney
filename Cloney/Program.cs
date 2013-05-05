namespace Cloney
{
    /// <summary>
    /// This class will start the Cloney program that is
    /// defined in the Cloney.Core library.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class Program
    {
        public static void Main(string[] args)
        {
            new Core.Program().Start(args);
        }
    }
}
