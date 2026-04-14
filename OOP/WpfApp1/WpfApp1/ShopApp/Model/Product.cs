using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WpfApp1.ShopApp.ModelView;

namespace WpfApp1.ShopApp.Model
{
    public class Product : BaseViewModel
    {
        public Guid Id { get; set; }

        private string _title;
        [Required, MaxLength(100)]
        public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }

        private string _description;
        public string Description { get => _description; set { _description = value; OnPropertyChanged(); } }

        private decimal _price;
        public decimal Price { get => _price; set { _price = value; OnPropertyChanged(); OnPropertyChanged(nameof(FinalPrice)); } }

        private double _discount;
        public double Discount { get => _discount; set { _discount = value; OnPropertyChanged(); OnPropertyChanged(nameof(FinalPrice)); } }

        private int _stockQuantity;
        public int StockQuantity { get => _stockQuantity; set { _stockQuantity = value; OnPropertyChanged(); } }

        private int _rating;
        public int Rating { get => _rating; set { _rating = value; OnPropertyChanged(); } }

        private byte[] _imageData;
        public byte[] ImageData { get => _imageData; set { _imageData = value; OnPropertyChanged(); } }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
        public decimal FinalPrice => Price - (Price * (decimal)Discount);

        [NotMapped]
        public bool HasDiscount => Discount > 0;

        [NotMapped]
        public string StatusColor => StockQuantity > 0 ? "Green" : "Red";
    }
}