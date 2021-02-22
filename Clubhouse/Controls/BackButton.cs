using Clubhouse.Navigation;

namespace Clubhouse.Controls
{
    public class BackButton : GlyphButton
    {
        public BackButton()
        {
            DefaultStyleKey = typeof(BackButton);
            Click += OnClick;
        }

        private void OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            BootStrapper.Current.RaiseBackRequested();
        }
    }
}
