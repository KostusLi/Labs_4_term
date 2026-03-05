using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.View;
using WpfApp1.ShopApp.ViewModels;
using WpfApp1.ShopApp.UndoRedo;

namespace WpfApp1.ShopApp.ModelView
{
    public class MainViewModel : BaseViewModel
    {
        private User _currentUser;
        private FileService _fileService;
        public RelayCommand SwitchToRuCommand { get; }
        public RelayCommand SwitchToEnCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand OpenProfileCommand { get; }

        private UndoRedoManager _historyManager;

        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }

        private ObservableCollection<Product> _allProducts;

        private ObservableCollection<Product> _displayProducts;

        public string CurrentUsername => _currentUser?.Username;
        public ObservableCollection<Product> DisplayProducts
        {
            get => _displayProducts;
            set { _displayProducts = value; OnPropertyChanged(); }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterProducts();
            }
        }

        public ObservableCollection<string> Categories { get; set; }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterProducts();
            }
        }

        private string _minPriceText;
        public string MinPriceText
        {
            get => _minPriceText;
            set
            {
                _minPriceText = value;
                OnPropertyChanged();
                FilterProducts();
            }
        }

        private string _maxPriceText;
        public string MaxPriceText
        {
            get => _maxPriceText;
            set
            {
                _maxPriceText = value;
                OnPropertyChanged();
                FilterProducts();
            }
        }

        public bool IsAdmin => _currentUser?.Role == Role.Admin;


        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public MainViewModel(User user)
        {
            _currentUser = user;
            _fileService = new FileService();

            _allProducts = new ObservableCollection<Product>();
            DisplayProducts = new ObservableCollection<Product>();
            Categories = new ObservableCollection<string>();

            SwitchToRuCommand = new RelayCommand(obj => App.ChangeLanguage("ru"));
            SwitchToEnCommand = new RelayCommand(obj => App.ChangeLanguage("en"));
            LogoutCommand = new RelayCommand(ExecuteLogout);

            var loadedProducts = _fileService.Load();

            if (loadedProducts.Count > 0)
            {
                foreach (var p in loadedProducts)
                    _allProducts.Add(p);
            }
            else
            {
                LoadMockData();
                SaveChanges();
            }

            UpdateCategories();
            FilterProducts();

            OpenProfileCommand = new RelayCommand(obj =>
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MainFrame.Navigate(new ProfilePage(_currentUser));
                }
            });

            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, obj => SelectedProduct != null);
            DeleteCommand = new RelayCommand(ExecuteDelete, obj => SelectedProduct != null);

            _historyManager = new UndoRedoManager();

            UndoCommand = new RelayCommand(
                execute: obj => { _historyManager.Undo(); FilterProducts(); },
                canExecute: obj => _historyManager.CanUndo
            );

            RedoCommand = new RelayCommand(
                execute: obj => { _historyManager.Redo(); FilterProducts(); },
                canExecute: obj => _historyManager.CanRedo
            );
        }

        private void ExecuteLogout(object obj)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainFrame.Navigate(new LoginPage());
            }
        }

        private void UpdateCategories()
        {
            Categories.Clear();
            Categories.Add("Все категории");

            var uniqueCategories = _allProducts.Select(p => p.Category).Distinct().OrderBy(c => c);

            foreach (var c in uniqueCategories)
            {
                if (!string.IsNullOrEmpty(c))
                    Categories.Add(c);
            }

            SelectedCategory = "Все категории";
        }

        private void FilterProducts()
        {
            var filtered = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(p => p.Title.ToLower().Contains(SearchText.ToLower()));
            }

            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "Все категории")
            {
                filtered = filtered.Where(p => p.Category == SelectedCategory);
            }

            if (decimal.TryParse(MinPriceText, out decimal minPrice))
            {
                filtered = filtered.Where(p => p.Price >= minPrice);
            }

            if (decimal.TryParse(MaxPriceText, out decimal maxPrice))
            {
                filtered = filtered.Where(p => p.Price <= maxPrice);
            }

            DisplayProducts = new ObservableCollection<Product>(filtered);
        }

        private void ExecuteAdd(object obj)
        {
            var addVM = new AddEditProductViewModel(null);

            var window = new AddEditProductWindow
            {
                DataContext = addVM
            };

            window.ShowDialog();

            if (addVM.DialogResult == true)
            {
                var newProduct = addVM.CurrentProduct;

                var action = new DelegateAction(
                    execute: () =>
                    {
                        _allProducts.Add(newProduct);
                        SaveChanges();
                        UpdateCategories();
                    },
                    undo: () =>
                    {
                        _allProducts.Remove(newProduct);
                        SaveChanges();
                        UpdateCategories();
                    }
                );

                _historyManager.ExecuteAction(action);
                FilterProducts();
            }
        }

        private void ExecuteEdit(object obj)
        {
            var editVM = new AddEditProductViewModel(SelectedProduct);
            var window = new AddEditProductWindow
            {
                DataContext = editVM
            };

            window.ShowDialog();

            if (editVM.DialogResult == true)
            {
                int index = _allProducts.IndexOf(SelectedProduct);
                if (index != -1)
                {
                    _allProducts[index] = editVM.CurrentProduct;
                    SaveChanges();
                    UpdateCategories();
                    FilterProducts();
                }
            }
        }

        private void ExecuteDelete(object obj)
        {
            var productToDelete = SelectedProduct;
            int index = _allProducts.IndexOf(productToDelete);

            var action = new DelegateAction(
                execute: () =>
                {
                    _allProducts.Remove(productToDelete);
                    SaveChanges();
                    UpdateCategories();
                },
                undo: () =>
                {
                    _allProducts.Insert(index, productToDelete);
                    SaveChanges();
                    UpdateCategories();
                }
            );

            _historyManager.ExecuteAction(action);
            FilterProducts();
        }

        private void LoadMockData()
        {
            _allProducts.Add(new Product { Title = "Ноутбук ASUS", Price = 75000, Category = "Техника", StockQuantity = 5 });
            _allProducts.Add(new Product { Title = "Мышь Logitech", Price = 3000, Category = "Аксессуары", StockQuantity = 10 });
            _allProducts.Add(new Product { Title = "Клавиатура Razer", Price = 8000, Category = "Аксессуары", StockQuantity = 0 });

            FilterProducts();
        }

        private void SaveChanges()
        {
            _fileService.Save(_allProducts);
        }
    }
}
