using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace Clubhouse.Common
{
    public static class Markdown
    {
        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(Markdown), new PropertyMetadata(null, OnMarkdownChanged));

        private static void OnMarkdownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as TextBlock;
            var markdown = e.NewValue as string;

            sender.Inlines.Clear();

            if (markdown == null)
            {
                return;
            }

            var previous = 0;
            var index = markdown.IndexOf("**");
            var next = index > -1 ? markdown.IndexOf("**", index + 2) : -1;

            while (index > -1 && next > -1)
            {
                if (index - previous > 0)
                {
                    sender.Inlines.Add(new Run { Text = markdown.Substring(previous, index - previous) });
                }

                var run = new Run { Text = markdown.Substring(index + 2, next - index - 2), FontWeight = FontWeights.SemiBold };

                sender.Inlines.Add(run);

                previous = next + 2;
                index = markdown.IndexOf("**", next + 2);
                next = index > -1 ? markdown.IndexOf("**", index + 2) : -1;
            }

            if (markdown.Length - previous > 0)
            {
                sender.Inlines.Add(new Run { Text = markdown.Substring(previous, markdown.Length - previous) });
            }
        }
    }
}
