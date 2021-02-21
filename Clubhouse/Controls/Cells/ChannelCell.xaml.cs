using Clubhouse.Models;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Controls.Cells
{
    public sealed partial class ChannelCell : UserControl
    {
        public ChannelCell()
        {
            InitializeComponent();
        }

        public Channel Channel
        {
            set => SetChannel(value);
        }

        private void SetChannel(Channel value)
        {
            if (value == null)
            {
                return;
            }

            var header = false;

            if (value.ClubName != null)
            {
                ClubName.Text = value.ClubName.ToUpper();
                ClubName.Visibility = Visibility.Visible;
                header = true;
            }
            else
            {
                ClubName.Visibility = Visibility.Collapsed;
            }

            if (value.Topic != null)
            {
                Topic.Text = value.Topic;
                Topic.Visibility = Visibility.Visible;
                header = true;
            }
            else
            {
                Topic.Visibility = Visibility.Collapsed;
            }

            ContentPanel.Margin = new Thickness(0, header ? 12 : 0, 0, 0);

            SetUsers(value.Users);

            Users.Children.Clear();

            foreach (var user in value.Users)
            {
                var textBlock = new TextBlock();
                textBlock.Text = user.Name;

                // This isn't the proper rule.
                if (value.ClubName == null && value.CreatorUserProfileId == user.UserId)
                {
                    textBlock.FontWeight = FontWeights.SemiBold;
                }
                else
                {
                    textBlock.FontWeight = FontWeights.Normal;
                }

                Users.Children.Add(textBlock);
            }

            All.Text = $"{value.NumAll}";
            Speakers.Text = $"{value.NumSpeakers}";
        }

        private void SetUsers(List<ChannelUser> value)
        {
            if (value.Count >= 2)
            {
                Picture1.Margin = new Thickness(24, 24, 12, 0);
                Picture1.Source = value[1].PhotoUrl;
                Picture2.Source = value[0].PhotoUrl;
            }
            else if (value.Count >= 1)
            {
                Picture1.Margin = new Thickness(24, 0, 12, 0);
                Picture1.Source = value[0].PhotoUrl;
                Picture2.Source = null;
            }
        }
    }
}
