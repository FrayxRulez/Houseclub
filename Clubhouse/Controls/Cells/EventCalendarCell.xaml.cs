using Clubhouse.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Controls.Cells
{
    public sealed partial class EventCalendarCell : UserControl
    {
        public EventCalendarCell()
        {
            InitializeComponent();
        }

        public Event Event
        {
            set => SetEvent(value);
        }

        private void SetEvent(Event value)
        {
            TimeStart.Text = value.timeStart.ToString("HH:mm");

            if (value.Club != null)
            {
                ClubName.Text = value.Club.Name.ToUpper();
                ClubName.Visibility = Visibility.Visible;
            }
            else
            {
                ClubName.Visibility = Visibility.Collapsed;
            }

            Name.Text = value.Name;
        }
    }
}
