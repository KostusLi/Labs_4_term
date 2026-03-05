using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp1.ShopApp.Model
{
    public class Product : INotifyPropertyChanged
    {
        private string _title;
        private decimal _price;
        private int _stockQuantity;
        private double _rating;
        private double _discount;
        private string _mainImagePath;

        public Guid Id { get; set; }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Manufacturer { get; set; }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalPrice));
                OnPropertyChanged(nameof(HasDiscount));
            }
        }

        public double Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FinalPrice));
                OnPropertyChanged(nameof(HasDiscount));
            }
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                _stockQuantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsInStock));
                OnPropertyChanged(nameof(StatusColor));
            }
        }

        public double Rating
        {
            get => _rating;
            set { _rating = value; OnPropertyChanged(); }
        }

        public string MainImagePath
        {
            get => _mainImagePath;
            set { _mainImagePath = value; OnPropertyChanged(); }
        }

        public List<string> AdditionalImages { get; set; } = new List<string>();

        public int SoldCount { get; set; }

        [JsonIgnore]
        public decimal FinalPrice => Price - (Price * (decimal)Discount);

        [JsonIgnore]
        public bool HasDiscount => Discount > 0;

        [JsonIgnore]
        public bool IsInStock => StockQuantity > 0;

        [JsonIgnore]
        public string StatusColor => IsInStock ? "Green" : "Red";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public Product()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
