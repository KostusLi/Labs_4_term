using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.ShopApp.View
{
    public partial class RatingControl : UserControl
    {
        public RatingControl()
        {
            InitializeComponent();
            UpdateStars(Value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(RatingControl),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RatingControl)d;
            control.UpdateStars((int)e.NewValue);
        }

        public static readonly RoutedEvent PreviewRatingChangedEvent = EventManager.RegisterRoutedEvent(
            "PreviewRatingChanged",
            RoutingStrategy.Tunnel,
            typeof(RoutedPropertyChangedEventHandler<int>),
            typeof(RatingControl));

        public event RoutedPropertyChangedEventHandler<int> PreviewRatingChanged
        {
            add { AddHandler(PreviewRatingChangedEvent, value); }
            remove { RemoveHandler(PreviewRatingChangedEvent, value); }
        }

        private void UpdateStars(int rating)
        {
            for (int i = 0; i < StarsPanel.Children.Count; i++)
            {
                var star = (TextBlock)StarsPanel.Children[i];
                star.Text = i < rating ? "★" : "☆";
            }
        }

        private void Star_MouseEnter(object sender, MouseEventArgs e)
        {
            var hoveredStar = (TextBlock)sender;
            int hoverValue = int.Parse(hoveredStar.Tag.ToString());
            UpdateStars(hoverValue);
        }

        private void StarsPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            UpdateStars(Value);
        }

        private void Star_Click(object sender, MouseButtonEventArgs e)
        {
            var clickedStar = (TextBlock)sender;
            int newValue = int.Parse(clickedStar.Tag.ToString());

            var args = new RoutedPropertyChangedEventArgs<int>(Value, newValue)
            {
                RoutedEvent = PreviewRatingChangedEvent
            };
            RaiseEvent(args);

            if (!args.Handled)
            {
                Value = newValue;
            }
        }
    }
}