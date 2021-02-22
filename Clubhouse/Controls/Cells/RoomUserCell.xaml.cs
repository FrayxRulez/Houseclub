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
        }

        public bool IsMuted
        {
            set => Muted.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool IsSpeaking
        {
            set => Speaking.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
