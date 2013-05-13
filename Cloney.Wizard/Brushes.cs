using System.Windows.Media;

namespace Cloney.Wizard
{
    public static class Brushes
    {
        public static Brush InputErrorBackgroundBrush
        {
            get { return new SolidColorBrush(Color.FromRgb(255, 99, 99)); }
        }

        public static Brush InputNeutralBackgroundBrush
        {
            get { return new SolidColorBrush(Color.FromRgb(255, 255, 255)); }
        }

        public static Brush InputWarningBackgroundBrush
        {
            get { return new SolidColorBrush(Color.FromRgb(255, 160, 0)); }
        }
    }
}
