using System;
using Cloney.Core.SolutionCloners;

namespace Cloney.Core.Tests.SolutionCloners
{
    internal class FakeSolutionCloner : SolutionClonerBase, ISolutionCloner
    {
        public void CloneSolution(string sourcePath, string targetPath)
        {
            OnCloningEnded(new EventArgs());
        }
    }
}