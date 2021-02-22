using Windows.UI.Xaml;

namespace Clubhouse.Common
{
    public static class Converters
    {
        public static Visibility NullToVisibility(object value)
        {
            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public static string Username(string text)
        {
            return $"@{text}";
        }

        public static string LastActive(int minutes)
        {
            if (minutes >= 60)
            {
                return $"{minutes / 60}h";
            }
            else if (minutes == 0)
            {
                return Strings.Resources.UserLastActiveOnline;
            }

            return $"{minutes}m";
        }

        public static double LastActiveOpacity(int minutes)
        {
            return minutes > 0 ? 0.5 : 1;
        }
    }
}
