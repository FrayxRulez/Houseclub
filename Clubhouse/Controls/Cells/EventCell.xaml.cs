using Clubhouse.Models;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Controls.Cells
{
    public sealed partial class EventCell : UserControl
    {
        public EventCell()
        {
            InitializeComponent();
        }

        public Event Event
        {
            set => SetEvent(value);
        }

        private void SetEvent(Event value)
        {
            TimeStart.Text = value.TimeStart.ToShortTimeString();
            Title.Text = value.Name;

            if (value.Club != null)
            {
                Club.Text = string.Format(Strings.Resources.EventFromClub, value.Club.Name);
                Club.Visibility = Visibility.Visible;
            }
            else
            {
                Club.Visibility = Visibility.Collapsed;
            }

            Pictures.ItemsSource = value.Hosts;

            Hosts.Text = string.Join(", ", value.Hosts.Select(x => x.Name));
            Description.Text = value.Description;
        }
    }
}
