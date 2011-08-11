using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Cloney
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                Process.Start("Cloney.Wizard.exe");
        }
    }
}
