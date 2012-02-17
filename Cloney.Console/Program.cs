using System;
using System.Collections.Generic;
using System.Globalization;
using Cloney.SubRoutines;
using NExtra;
using NExtra.Localization;

namespace Cloney.Console
{
    /// <summary>
    /// This class will start the Cloney application that
    /// is defined in the Cloney library.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class Program
    {
        private static void Main(string[] args)
        {
            var a = new Cloney.Program(new C(), new T(), new  A(), new S());
            //new Cloney.Program().Start(args);
        }
    }


    public class S : ISubRoutineLocator
    {
        public IEnumerable<ISubRoutine> FindAll()
        {
            throw new NotImplementedException();
        }
    }
    public class A : ICommandLineArgumentParser {
        public IDictionary<string, string> ParseCommandLineArguments(IEnumerable<string> args)
        {
            throw new NotImplementedException();
        }
    }
    
    public class T : ITranslator {
        public string Translate(string key)
        {
            throw new NotImplementedException();
        }

        public string Translate(string key, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public bool TranslationExists(string key)
        {
            throw new NotImplementedException();
        }

        public bool TranslationExists(string key, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
    public class C : IConsole {
        public int Read()
        {
            throw new NotImplementedException();
        }

        public ConsoleKeyInfo ReadKey()
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void Write(string value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string value)
        {
            throw new NotImplementedException();
        }
    }
}
