using Clubhouse.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Controls.Cells
{
    public sealed partial class RoomUserCell : UserControl
    {
        public RoomUserCell()
        {
            InitializeComponent();
        }

        public ChannelUser User
        {
            set => SetUser(value);
        }

        private void SetUser(ChannelUser value)
        {
            Picture.Source = value;
            Name.Text = value.FirstName;

            IsMuted = value.isMuted;

            if (New == null)
            {
                if (value.IsNew)
                {
                    FindName(nameof(New));
                }
                else
                {
                    return;
                }
            }

            New.Visibility = value.IsNew ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool IsMuted
        {
            set => SetMuted(value);
        }

        private void SetMuted(bool value)
        {
            if (Muted == null)
            {
                if (value)
                {
                    FindName(nameof(Muted));
                }
                else
                {
                    return;
                }
            }

            Muted.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool IsSpeaking
        {
            set => SetSpeaking(value);
        }

        private void SetSpeaking(bool value)
        {
            if (Speaking == null)
            {
                if (value)
                {
                    FindName(nameof(Speaking));
                }
                else
                {
                    return;
                }
            }

            Speaking.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
