using System;
using System.Threading;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using AtrumSoft.UWP.Controls.Tools;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace AtrumSoft.UWP.Controls
{
    public sealed class CircularProgressBar : Control
    {
        public CircularProgressBar()
        {
            this.DefaultStyleKey = typeof(CircularProgressBar);
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Brush), typeof(CircularProgressBar), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

        public Brush Color
        {
            get { return (Brush) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(default(double), ValueChangedCallback));

        private static void ValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var circularProgresBar = dependencyObject as CircularProgressBar;

            var segment = circularProgresBar?.GetTemplateChild("PartArc") as ArcSegment;
            if (segment == null)
                return;

            var textBlock = circularProgresBar.GetTemplateChild("PartTextBlock") as TextBlock;

            var value = (double) dependencyPropertyChangedEventArgs.NewValue;
            var angle = CircleUtils.GetAngleFor(value);
            var newPoint = GetNextPoint(angle, segment.Size.Width);

            SynchronizationContext.Current.Post(o =>
            {
                segment.IsLargeArc = angle >= Math.PI;
                segment.Point = newPoint;
                if (textBlock != null)
                    textBlock.Text = $"{value*100} %";
            },null);
        }

        private static Point GetNextPoint(double angle, double size)
        {
            var x = Math.Sin(angle)*size + size;
            var y = (Math.Cos(angle)*size - size)*-1;
            var newPoint = new Point(x, y);
            return newPoint;
        }


        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
    
    
}
