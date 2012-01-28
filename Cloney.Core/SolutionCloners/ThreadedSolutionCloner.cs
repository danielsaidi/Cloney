using System;
using System.Threading;

namespace Cloney.Core.SolutionCloners
{
    /// <summary>
    /// This class can be used to clone Visual Studio solutions
    /// on a separate thread, using another solution cloner.
    /// </summary>
    public class ThreadedSolutionCloner : SolutionClonerBase, ISolutionCloner
    {
        private readonly ISolutionCloner solutionCloner;


        public ThreadedSolutionCloner(ISolutionCloner solutionCloner)
        {
            this.solutionCloner = solutionCloner;
        }


        public new string CurrentPath
        {
            get { return solutionCloner.CurrentPath; }
        }


        public void CloneSolution(string sourcePath, string targetPath)
        {
            new Thread(() =>
            {
                OnCloningBegun(new EventArgs());
                solutionCloner.CloneSolution(sourcePath, targetPath);
                OnCloningEnded(new EventArgs());
            }).Start();
        }
    }
}
