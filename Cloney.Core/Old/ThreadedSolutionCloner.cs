using System;
using System.Threading;
using Cloney.Core.Old.Abstractions;

namespace Cloney.Core.Old
{
    /// <summary>
    /// This class can be used to clone a Visual Studio solution
    /// by running another ICanCloneSolution implementation on a
    /// separate thread.
    /// </summary>
    public class ThreadedSolutionCloner : ICanCloneSolution
    {
        private readonly ICanCloneSolution solutionCloner;


        public ThreadedSolutionCloner(ICanCloneSolution solutionCloner)
        {
            this.solutionCloner = solutionCloner;
        }


        public event EventHandler CloningBegun;

        public event EventHandler CloningEnded;


        public string CurrentPath { get { return solutionCloner.CurrentPath; } }


        public void CloneSolution(string sourcePath, string sourceNamespace, string targetPath, string targetNamespace)
        {
            new Thread(() =>
            {
                OnCloningBegun(null);
                solutionCloner.CloneSolution(sourcePath, sourceNamespace, targetPath, targetNamespace);
                OnCloningEnded(null);
            }).Start();
        }

        private void OnCloningBegun(EventArgs e)
        {
            if (CloningBegun != null) CloningBegun(this, e);
        }

        private void OnCloningEnded(EventArgs e)
        {
            if (CloningEnded != null) CloningEnded(this, e);
        }
    }
}