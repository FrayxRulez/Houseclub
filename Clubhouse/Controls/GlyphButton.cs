﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Clubhouse.Controls
{
    public class GlyphButton : Button
    {
        public GlyphButton()
        {
            DefaultStyleKey = typeof(GlyphButton);
        }

        #region Glyph
        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register("Glyph", typeof(string), typeof(GlyphButton), new PropertyMetadata(null));
        #endregion
    }

    public class GlyphHyperlinkButton : HyperlinkButton
    {
        public GlyphHyperlinkButton()
        {
            DefaultStyleKey = typeof(GlyphHyperlinkButton);
        }

        #region Glyph
        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register("Glyph", typeof(string), typeof(GlyphHyperlinkButton), new PropertyMetadata(null));
        #endregion
    }

}
