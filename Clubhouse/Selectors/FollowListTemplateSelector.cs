using Clubhouse.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Selectors
{
    public class FollowListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ClubTemplate { get; set; }

        public DataTemplate UserTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return item switch
            {
                Club _ => ClubTemplate,
                User _ => UserTemplate,
                _ => base.SelectTemplateCore(item, container),
            };
        }
    }
}
