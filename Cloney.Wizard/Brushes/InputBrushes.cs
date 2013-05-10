using System.Windows.Media;

namespace Cloney.Wizard.Brushes
{
    public static class InputBrushes
    {
        public static Brush ErrorBackgroundBrush
        {
            get { return new SolidColorBrush(Color.FromRgb(255, 99, 99)); }
        }

        public static Brush WarningBackgroundBrush
        {
            get { return new SolidColorBrush(Color.FromRgb(255, 160, 0)); }
        }
    }
}
