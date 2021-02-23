using Clubhouse.Models;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Globalization;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Imaging;

namespace Clubhouse.Controls
{
    public class ProfilePicture : HyperlinkButton
    {
        private TextBlock InitialsPart;
        private Image ImagePart;

        private object _source;

        public ProfilePicture()
        {
            DefaultStyleKey = typeof(ProfilePicture);
        }

        protected override void OnApplyTemplate()
        {
            ImagePart = GetTemplateChild("ImagePart") as Image;
            OnSourceChanged(Source, _source);
            base.OnApplyTemplate();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            UpdateShape(finalSize.Width, finalSize.Height);
            return base.ArrangeOverride(finalSize);
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

        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(ProfilePicture), new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProfilePicture)d).OnSourceChanged(e.NewValue, e.OldValue);
        }

        private void OnSourceChanged(object newValue, object oldValue)
        {
            if (newValue == _source)
            {
                return;
            }

            Uri photoUrl = null;
            String name = null;
            
            if (newValue is User user)
            {
                photoUrl = user.PhotoUrl;
                name = user.Name;
            }
            else if (newValue is Club club)
            {
                photoUrl = club.PhotoUrl;
                name = club.Name;
            }

            if (photoUrl != null)
            {
                if (InitialsPart != null)
                {
                    InitialsPart.Text = string.Empty;
                }

                if (ImagePart != null)
                {
                    _source = newValue;
                    ImagePart.Source = new BitmapImage(photoUrl);
                }
            }
            else if (name != null)
            {
                if (InitialsPart == null)
                {
                    InitialsPart = GetTemplateChild("InitialsPart") as TextBlock;
                }

                if (InitialsPart != null)
                {
                    _source = newValue;
                    InitialsPart.Text = Convert(name);
                }

                if (ImagePart != null)
                {
                    ImagePart.Source = null;
                }
            }
            else
            {
                if (InitialsPart != null)
                {
                    InitialsPart.Text = string.Empty;
                }

                if (ImagePart != null)
                {
                    ImagePart.Source = null;
                }

                _source = null;
            }
        }

        #endregion

        private static string Convert(string name)
        {
            var words = name.Split(new char[] { ' ' });
            if (words.Length > 1)
            {
                return Convert(words[0], words[words.Length - 1]);
            }
            else if (words.Length > 0)
            {
                return Convert(words[0], string.Empty);
            }

            return string.Empty;
        }

        private static string Convert(string word1, string word2)
        {
            word1 = word1 ?? string.Empty;
            word2 = word2 ?? string.Empty;

            var si1 = StringInfo.GetTextElementEnumerator(word1);
            var si2 = StringInfo.GetTextElementEnumerator(word2);

            word1 = si1.MoveNext() ? si1.GetTextElement() : string.Empty;
            word2 = si2.MoveNext() ? si2.GetTextElement() : string.Empty;

            return string.Format("{0}{1}", word1, word2).Trim().ToUpperInvariant();
        }
    }
}
