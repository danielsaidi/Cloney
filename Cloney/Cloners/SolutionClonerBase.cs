using System;

namespace Cloney.Cloners
{
    /// <summary>
    /// This abstract base class provide solution cloners
    /// with basic functionality.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class SolutionClonerBase
    {
        private string currentPath;


        public string CurrentPath
        {
            get { return currentPath; }
            set
            {
                currentPath = value;
                OnCurrentPathChanged(new EventArgs());
            }
        }


        public event EventHandler CloningBegun;

        public event EventHandler CloningEnded;

        public event EventHandler CurrentPathChanged;


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

        public void OnCurrentPathChanged(EventArgs e)
        {
            if (CurrentPathChanged != null)
                CurrentPathChanged(this, e);
        }
    }
}
