using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Designer
{
    public sealed class BoundsDragDeltaControl2 : ContentControl
    {
        private Thumb thumb;

        public BoundsDragDeltaControl2()
        {
            this.DefaultStyleKey = typeof(BoundsDragDeltaControl2);
        }

        protected override void OnApplyTemplate()
        {
            Thumb = (Thumb)GetTemplateChild("Thumb");
            base.OnApplyTemplate();
        }

        private void ThumbOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            HandleHorizontal(e);
            HandleVertical(e);
        }

        private void HandleHorizontal(DragDeltaEventArgs e)
        {
            var horzDelta = e.HorizontalChange;

            if (X1 <= X2)
            {
                if (Width + horzDelta > 0)
                {
                    Width += horzDelta;
                    X2 += horzDelta;
                }
                else
                {
                    X1 -= horzDelta;
                    Width -= horzDelta;
                    Left += horzDelta;
                }
            }
            else
            {
                if (Width - horzDelta > 0)
                {
                    X1 -= horzDelta;
                    Width -= horzDelta;
                    Left += horzDelta;
                }
                else
                {
                    Width += horzDelta;
                    X2 += horzDelta;
                }
            }
        }

        private void HandleVertical(DragDeltaEventArgs e)
        {
            var delta = e.VerticalChange;

            if (Y1 <= Y2)
            {
                if (Height + delta > 0)
                {
                    Height += delta;
                    Y2 += delta;
                }
                else
                {
                    Y1 -= delta;
                    Height -= delta;
                    Top += delta;
                }
            }
            else
            {
                if (Height - delta > 0)
                {
                    Y1 -= delta;
                    Height -= delta;
                    Top += delta;
                }
                else
                {
                    Height += delta;
                    Y2 += delta;
                }
            }
        }

        public static readonly DependencyProperty X1Property = DependencyProperty.Register(
            "X1", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double X1
        {
            get { return (double) GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        public static readonly DependencyProperty X2Property = DependencyProperty.Register(
            "X2", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double X2
        {
            get { return (double) GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(
            "Left", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double Left
        {
            get { return (double) GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double Width
        {
            get { return (double) GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DependencyProperty Y1Property = DependencyProperty.Register(
            "Y1", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double Y1
        {
            get { return (double) GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        public static readonly DependencyProperty Y2Property = DependencyProperty.Register(
            "Y2", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double Y2
        {
            get { return (double) GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(
            "Top", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double Top
        {
            get { return (double) GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
            "Height", typeof(double), typeof(BoundsDragDeltaControl2), new PropertyMetadata(default(double)));

        public double Height
        {
            get { return (double) GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
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
    }
}