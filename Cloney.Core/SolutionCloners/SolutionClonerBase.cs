using System;

namespace Cloney.Core.SolutionCloners
{
    /// <summary>
    /// This abstract base class can provide solution
    /// cloners with basic functionality.
    /// </summary>
    public class SolutionClonerBase
    {
        public event EventHandler CloningBegun;
        
        public event EventHandler CloningEnded;


        public void OnCloningBegun(EventArgs e)
        {
            if (CloningBegun != null)
                CloningBegun(this, e);
        }

        public void OnCloningEnded(EventArgs e)
        {
            if (CloningEnded != null)
                CloningEnded(this, e);
        }
    }
}
