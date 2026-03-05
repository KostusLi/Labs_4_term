using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ViewModels;

namespace WpfApp1.ShopApp.ModelView
{
    public class AddEditProductViewModel: BaseViewModel
    {
        private Product _currentProduct;
        public Product CurrentProduct
        {
            get => _currentProduct;
            set { _currentProduct = value; OnPropertyChanged(); }
        }

        public string WindowTitle { get; set; }

        public bool DialogResult { get; private set; } = false;

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public AddEditProductViewModel(Product product = null)
        {
            if (product == null)
            {
                WindowTitle = "Добавление нового товара";
                CurrentProduct = new Product { Price = 0, Discount = 0, StockQuantity = 1 };
            }
            else
            {
                WindowTitle = "Редактирование товара";
                CurrentProduct = new Product
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    Discount = product.Discount,
                    StockQuantity = product.StockQuantity,
                    MainImagePath = product.MainImagePath
                };
            }

            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private bool CanExecuteSave(object obj)
        {
            return !string.IsNullOrWhiteSpace(CurrentProduct.Title) && CurrentProduct.Price >= 0;
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
