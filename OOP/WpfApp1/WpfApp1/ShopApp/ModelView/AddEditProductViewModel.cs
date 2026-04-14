using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;

namespace WpfApp1.ShopApp.ModelView
{
    public class AddEditProductViewModel : BaseViewModel
    {
        public RelayCommand SelectImageCommand { get; }

        private Product _currentProduct;
        public Product CurrentProduct
        {
            get => _currentProduct;
            set { _currentProduct = value; OnPropertyChanged(); }
        }

        public List<Category> AvailableCategories { get; set; }

        public string WindowTitle { get; set; }
        public bool DialogResult { get; private set; } = false;

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public AddEditProductViewModel(Product product, List<Category> categories)
        {
            AvailableCategories = categories;

            if (product == null)
            {
                WindowTitle = "Добавление нового товара";
                CurrentProduct = new Product { Price = 0, Discount = 0, StockQuantity = 1 };

                if (categories.Count > 0) CurrentProduct.CategoryId = categories[0].Id;
            }
            else
            {
                WindowTitle = "Редактирование товара";
                CurrentProduct = new Product
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Discount = product.Discount,
                    StockQuantity = product.StockQuantity,
                    Rating = product.Rating,
                    ImageData = product.ImageData
                };
            }

            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
            SelectImageCommand = new RelayCommand(ExecuteSelectImage);
        }

        private void ExecuteSelectImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите фотографию товара",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                CurrentProduct.ImageData = imageBytes;
            }
        }

        private bool CanExecuteSave(object obj)
        {
            return !string.IsNullOrWhiteSpace(CurrentProduct.Title)
                   && CurrentProduct.Price >= 0
                   && CurrentProduct.CategoryId > 0;
        }

        private void ExecuteSave(object parameter)
        {
            DialogResult = true;
            if (parameter is Window window)
            {
                window.DialogResult = true;
            }
        }

        private void ExecuteCancel(object parameter)
        {
            DialogResult = false;
            if (parameter is Window window)
            {
                window.DialogResult = false;
            }
        }
    }
}