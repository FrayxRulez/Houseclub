using Clubhouse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Clubhouse.Controls.Cells
{
    public sealed partial class EventCalendarCell : UserControl
    {
        public EventCalendarCell()
        {
            this.InitializeComponent();
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
