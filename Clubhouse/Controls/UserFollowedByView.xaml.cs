using Clubhouse.Common;
using Clubhouse.Models;
using System;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Controls
{
    public sealed partial class UserFollowedByView : UserControl
    {
        public UserFollowedByView()
        {
            InitializeComponent();
        }

        public ICommand Command
        {
            set => Button.Command = value;
        }

        public FullUser FullUser
        {
            set => SetFullUser(value);
        }

        private void SetFullUser(FullUser value)
        {
            if (value?.MutualFollows != null && value.MutualFollows.Count > 0)
            {
                var names = string.Join(", ", value.MutualFollows.Select(x => x.Name));

                if (value.MutualFollowsCount > value.MutualFollows.Count)
                {
                    var count = $"{value.MutualFollowsCount - value.MutualFollows.Count}";
                    Markdown.SetText(Text, string.Format(Strings.Resources.UserFollowedByAnd, names, count));
                }
                else
                {
                    Markdown.SetText(Text, string.Format(Strings.Resources.UserFollowedBy, names));
                }

                Pictures.Children.Clear();

                for (int i = 0; i < Math.Min(3, value.MutualFollows.Count); i++)
                {
                    var picture = new ProfilePicture();
                    picture.Source = value.MutualFollows[i];
                    picture.Width = 36;
                    picture.Height = 36;
                    picture.Margin = new Thickness(i > 0 ? -8 : 0, 0, 0, 0);

                    Canvas.SetZIndex(picture, value.MutualFollows.Count - i);
                    Pictures.Children.Add(picture);
                }

                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
    }
}
