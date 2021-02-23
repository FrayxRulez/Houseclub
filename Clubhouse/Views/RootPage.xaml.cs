using System;
using System.Numerics;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.Views
{
    public sealed partial class RootPage : UserControl
    {
        public RootPage()
        {
            InitializeComponent();

            PopupFrame.Navigate(typeof(BlankPage));
            PopupFrame.Navigated += PopupFrame_Navigated;

            if (ApiInformation.IsPropertyPresent("Windows.UI.Xaml.UIElement", "Shadow"))
            {
                var themeShadow = new ThemeShadow();
                PopupElement.Shadow = themeShadow;
                PopupElement.Translation += new Vector3(0, 0, 32);

                themeShadow.Receivers.Add(PopupShadow);
            }
        }

        private void PopupFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.SourcePageType == typeof(BlankPage))
            {
                PopupRoot.Visibility = Visibility.Collapsed;
            }
            else
            {
                PopupRoot.Visibility = Visibility.Visible;
            }
        }

        public Frame Frame => RootFrame;
        public Frame Popup => PopupFrame;

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PopupElement.Height = Math.Min(500, e.NewSize.Height);
        }
    }
}
