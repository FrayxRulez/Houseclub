using System.Collections;
using Windows.UI.Xaml;

namespace Clubhouse.Common
{
    public static class Converters
    {
        public static Visibility NullToVisibility(object value)
        {
            if (value is string str)
            {
                return string.IsNullOrWhiteSpace(str) ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (value is IList list)
            {
                return list.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }

            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public static Visibility Negation(bool value)
        {
            return value ? Visibility.Collapsed : Visibility.Visible;
        }

        public static string ShortNumber(int number)
        {
            var K = string.Empty;
            var lastDec = 0;

            while (number / 1000 > 0)
            {
                K += "K";
                lastDec = (number % 1000) / 100;
                number /= 1000;
            }

            if (lastDec != 0 && K.Length > 0)
            {
                if (K.Length == 2)
                {
                    return string.Format("{0}.{1}M", number, lastDec);
                }
                else
                {
                    return string.Format("{0}.{1}{2}", number, lastDec, K);
                }
            }

            if (K.Length == 2)
            {
                return string.Format("{0}M", number);
            }
            else
            {
                return string.Format("{0}{1}", number, K);
            }
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
