﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Zafiro.Designer
{
    public sealed class DeltaContentControl : ContentControl
    {
        private Thumb thumb;

        public DeltaContentControl()
        {
            this.DefaultStyleKey = typeof(DeltaContentControl);
        }

        public Thumb Thumb
        {
            get { return thumb; }
            private set
            {
                thumb = value;
                thumb.DragDelta += ThumbOnDragDelta;
            }
        }

        private void ThumbOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (AllowHorizontal)
            {
                var newValue = Horizontal + e.HorizontalChange;
                if (newValue > 0)
                {
                    Horizontal = newValue;
                }
            }

            if (AllowVertical)
            {
                var newValue = Vertical + e.VerticalChange;
                if (newValue > 0)
                {
                    Vertical = newValue;
                }
            }
        }

        protected override void OnApplyTemplate()
        {
            Thumb = (Thumb)GetTemplateChild("Thumb");
            base.OnApplyTemplate();
        }

        public static readonly DependencyProperty HorizontalProperty = DependencyProperty.Register(
            "Horizontal", typeof(double), typeof(DeltaContentControl), new PropertyMetadata(default(double)));

        public double Horizontal
        {
            get { return (double)GetValue(HorizontalProperty); }
            set { SetValue(HorizontalProperty, value); }
        }

        public static readonly DependencyProperty VerticalProperty = DependencyProperty.Register(
            "Vertical", typeof(double), typeof(DeltaContentControl), new PropertyMetadata(default(double)));

        public double Vertical
        {
            get { return (double)GetValue(VerticalProperty); }
            set { SetValue(VerticalProperty, value); }
        }

        public static readonly DependencyProperty AllowVerticalProperty = DependencyProperty.Register(
            "AllowVertical", typeof(bool), typeof(DeltaContentControl), new PropertyMetadata(true));

        public bool AllowVertical
        {
            get { return (bool)GetValue(AllowVerticalProperty); }
            set { SetValue(AllowVerticalProperty, value); }
        }

        public static readonly DependencyProperty AllowHorizontalProperty = DependencyProperty.Register(
            "AllowHorizontal", typeof(bool), typeof(DeltaContentControl), new PropertyMetadata(true));

        public bool AllowHorizontal
        {
            get { return (bool)GetValue(AllowHorizontalProperty); }
            set { SetValue(AllowHorizontalProperty, value); }
        }
    }
}