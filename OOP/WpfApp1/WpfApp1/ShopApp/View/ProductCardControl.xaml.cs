using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.ShopApp.Model;

namespace WpfApp1.ShopApp.View
{
    public partial class ProductCardControl : UserControl
    {
        public static readonly RoutedUICommand ResetRatingCmd = new RoutedUICommand(
            "Сбросить рейтинг",
            "ResetRatingCmd",
            typeof(ProductCardControl),
            new InputGestureCollection()
            {
                new KeyGesture(Key.R, ModifierKeys.Control)
            }
        );

        public ProductCardControl()
        {
            InitializeComponent();
        }

        private void ResetRatingCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.DataContext is Product currentProduct)
            {
                currentProduct.Rating = 0;
            }
        }

        private void CardRating_PreviewRatingChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if (e.NewValue == 1)
            {
                MessageBox.Show("Оценка в 1 звезду запрещена политикой магазина!", "Туннелирование (Tunneling)");
                e.Handled = true;
            }
        }
    }
}