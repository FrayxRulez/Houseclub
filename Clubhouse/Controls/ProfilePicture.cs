using Microsoft.Graphics.Canvas.Geometry;
using System;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Clubhouse.Controls
{
    public class ProfilePicture : HyperlinkButton
    {
        public ProfilePicture()
        {
            DefaultStyleKey = typeof(ProfilePicture);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            UpdateClip(finalSize.Width, finalSize.Height);
            return base.ArrangeOverride(finalSize);
        }

        private void UpdateClip(double width, double height, double eccentricity = 2.5)
        {
            double x = 0;
            double y = 0;

            double halfWidth = width / 2.0;
            double halfHeight = height / 2.0;

            double centreX = x + halfWidth;
            double centreY = y + halfHeight;

            double TWO_PI = Math.PI * 2.0;
            int resolution = 100;

            var builder = new CanvasPathBuilder(null);
            builder.BeginFigure((float)(x + width), (float)centreY);

            for (double theta = 0.0; theta < TWO_PI; theta += TWO_PI / resolution)
            {
                double sineTheta = Math.Sin(theta);
                double cosineTheta = Math.Cos(theta);
                double r =
                    Math.Pow(1 / (Math.Pow(Math.Abs(cosineTheta) / halfWidth, eccentricity) + Math.Pow(Math.Abs(sineTheta) / halfHeight, eccentricity)),
                        1 / eccentricity);
                builder.AddLine((float)(centreX + r * cosineTheta), (float)(centreY + r * sineTheta));
            }

            builder.EndFigure(CanvasFigureLoop.Closed);

            var path = new CompositionPath(CanvasGeometry.CreatePath(builder));
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
