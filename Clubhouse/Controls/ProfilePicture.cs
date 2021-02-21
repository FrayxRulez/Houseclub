using Clubhouse.Common;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace Clubhouse.Controls
{
    public class ProfilePicture : HyperlinkButton
    {
        public ProfilePicture()
        {
            DefaultStyleKey = typeof(ProfilePicture);

            var builder = new CanvasPathBuilder(null);
            /* M18.947 18.947
             * C5.627 32.267 1 61.517 1 128
             * s4.626 95.732 17.947 109.053
             * C32.267 250.373 61.517 255 128 255
             * s95.732-4.626 109.053-17.947
             * C250.373 223.733 255 194.483 255 128
             * s-4.626-95.732-17.947-109.053
             * C223.733 5.627 194.483 1 128 1
             * S32.268 5.626 18.947 18.947z
             */

            var path = CompositionPathParser.Parse("M3.4,3.4C0.9,5.9,0,11.4,0,24s0.9,18.1,3.4,20.6c2.5,2.5,8,3.4,20.6,3.4s18.1-0.9,20.6-3.4c2.5-2.5,3.4-8,3.4-20.6S47.1,5.9,44.6,3.4C42.1,0.9,36.6,0,24,0S5.9,0.9,3.4,3.4z");
            var visual = ElementCompositionPreview.GetElementVisual(this);
            visual.Clip = Window.Current.Compositor.CreateGeometricClip(Window.Current.Compositor.CreatePathGeometry(path));
        }

        #region Source

        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(ProfilePicture), new PropertyMetadata(null));

        #endregion
    }
}
