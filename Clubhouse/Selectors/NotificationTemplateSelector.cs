using Clubhouse.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Selectors
{
    public class NotificationTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ActionableNotificationTemplate { get; set; }

        public DataTemplate NotificationTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return item switch
            {
                ActionableNotification _ => ActionableNotificationTemplate,
                Notification _ => NotificationTemplate,
                _ => base.SelectTemplateCore(item, container),
            };
        }
    }
}
