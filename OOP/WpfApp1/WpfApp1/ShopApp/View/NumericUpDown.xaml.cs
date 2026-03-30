using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.ShopApp.View
{
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(int),
                typeof(NumericUpDown),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    OnValueChanged,           
                    CoerceValue),            
                ValidateValue);               

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericUpDown), new PropertyMetadata(100));

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        private static bool ValidateValue(object value)
        {
            return value is int;
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            NumericUpDown control = (NumericUpDown)d;
            int newValue = (int)baseValue;

            if (newValue < control.MinValue)
                return control.MinValue;

            if (newValue > control.MaxValue)
                return control.MaxValue;

            return newValue;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                Regex regex = new Regex("[^0-9]+");
                if (regex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            "ValueChanged",            
            RoutingStrategy.Bubble,   
            typeof(RoutedPropertyChangedEventHandler<int>),
            typeof(NumericUpDown));

        public event RoutedPropertyChangedEventHandler<int> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown control = (NumericUpDown)d;
            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;

            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue);
            args.RoutedEvent = ValueChangedEvent;
            control.RaiseEvent(args);
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }

    }
}