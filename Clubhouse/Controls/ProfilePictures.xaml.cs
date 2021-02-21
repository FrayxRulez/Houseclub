using Clubhouse.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Controls
{
    public sealed partial class ProfilePictures : UserControl
    {
        public ProfilePictures()
        {
            this.InitializeComponent();
        }

        public List<ChannelUser> Users
        {
            set => SetUsers(value);
        }

        private void SetUsers(List<ChannelUser> value)
        {
            if (value.Count >= 2)
            {
                Picture1.Margin = new Thickness(24, 24, 0, 0);
                Picture1.Source = new Uri(value[1].PhotoUrl);
                Picture2.Source = new Uri(value[0].PhotoUrl);
            }
            else if (value.Count >= 1)
            {
                Picture1.Margin = new Thickness(24, 0, 0, 0);
                Picture1.Source = new Uri(value[0].PhotoUrl);
                Picture2.Source = null;
            }
        }
    }
}
