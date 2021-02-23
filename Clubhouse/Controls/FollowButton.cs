using Windows.UI.Xaml.Controls.Primitives;

namespace Clubhouse.Controls
{
    public class FollowButton : ToggleButton
    {
        public FollowButton()
        {
            DefaultStyleKey = typeof(FollowButton);

            Checked += OnToggle;
            Unchecked += OnToggle;
        }

        private void OnToggle(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Content = IsChecked == true
                ? Strings.Resources.Following
                : Strings.Resources.Follow;
        }

        protected override void OnToggle()
        {
            // We don't want to change the state when the user clicks
            // ViewModels will take care of the actual state

            //base.OnToggle();
        }
    }
}
