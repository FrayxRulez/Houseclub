using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Clubhouse.Controls
{
    public class Superellipse : Path
    {
        public Superellipse()
        {
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            UpdateShape(e.NewSize.Width, e.NewSize.Height);
        }

        private void UpdateShape(double width, double height, double eccentricity = 2.5)
        {
            double x = 0;
            double y = 0;

            double halfWidth = width / 2.0;
            double halfHeight = height / 2.0;

            double centreX = x + halfWidth;
            double centreY = y + halfHeight;

            double TWO_PI = Math.PI * 2.0;
            int resolution = 100;

            var figure = new PathFigure();
            figure.StartPoint = new Point(x + width, centreY);

            for (double theta = 0.0; theta < TWO_PI; theta += TWO_PI / resolution)
            {
                double sineTheta = Math.Sin(theta);
                double cosineTheta = Math.Cos(theta);
                double r =
                    Math.Pow(1 / (Math.Pow(Math.Abs(cosineTheta) / halfWidth, eccentricity) + Math.Pow(Math.Abs(sineTheta) / halfHeight, eccentricity)),
                        1 / eccentricity);
                figure.Segments.Add(new LineSegment { Point = new Point(centreX + r * cosineTheta, centreY + r * sineTheta) });
            }

            var path = new PathGeometry();
            path.Figures.Add(figure);

            Data = path;
        }
    }
}
