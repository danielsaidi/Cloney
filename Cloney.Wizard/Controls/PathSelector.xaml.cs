using System.Windows;
using System.Windows.Controls;

namespace Cloney.Wizard.Controls
{
    /// <summary>
    /// This component can be used to select a folder by
    /// using a native folder selector.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public partial class PathSelector
    {
        public PathSelector()
        {
            InitializeComponent();
        }


        protected override void Refresh()
        {
            base.Refresh();
            txtFolder.Text = Path;
            txtFolder.Background = HasValidPath ? Brushes.InputNeutralBackgroundBrush : Brushes.InputErrorBackgroundBrush;
        }


        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenPathSelector();
        }

        private void textBox_Changed(object sender, TextChangedEventArgs e)
        {
            SetPath(((TextBox)sender).Text);
        }

        private void txtFolder_LostFocus(object sender, RoutedEventArgs e)
        {
            SetPath(((TextBox)sender).Text);
        }
    }
}
