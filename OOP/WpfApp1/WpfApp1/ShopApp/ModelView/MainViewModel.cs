using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Database;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;
using WpfApp1.ShopApp.UndoRedo;
using WpfApp1.ShopApp.View;

namespace WpfApp1.ShopApp.ModelView
{
    public class MainViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly ProductRepository _repository;
        private readonly UndoRedoManager _historyManager;

        private ObservableCollection<Product> _allProducts;
        private ObservableCollection<Product> _displayProducts;

        public RelayCommand SwitchToRuCommand { get; }
        public RelayCommand SwitchToEnCommand { get; }
        public RelayCommand OpenProfileCommand { get; }
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand OpenDbAdminCommand { get; }

        public string CurrentUsername => _currentUser?.Username;
        public bool IsAdmin => _currentUser?.Role == Role.Admin;

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

        public ObservableCollection<string> Categories { get; set; }

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterProductsAsync();
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterProductsAsync();
            }
        }

        private string _minPriceText;
        public string MinPriceText
        {
            get => _minPriceText;
            set { _minPriceText = value; OnPropertyChanged(); FilterProductsAsync(); }
        }

        private string _maxPriceText;
        public string MaxPriceText
        {
            get => _maxPriceText;
            set { _maxPriceText = value; OnPropertyChanged(); FilterProductsAsync(); }
        }


        public MainViewModel(User user)
        {
            _currentUser = user;
            _repository = new ProductRepository();
            _historyManager = new UndoRedoManager();

            _allProducts = new ObservableCollection<Product>();
            DisplayProducts = new ObservableCollection<Product>();
            Categories = new ObservableCollection<string>();

            SwitchToRuCommand = new RelayCommand(obj => App.ChangeLanguage("ru"));
            SwitchToEnCommand = new RelayCommand(obj => App.ChangeLanguage("en"));
            LogoutCommand = new RelayCommand(ExecuteLogout);

            OpenProfileCommand = new RelayCommand(obj =>
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                    mainWindow.MainFrame.Navigate(new ProfilePage(_currentUser));
            });

            UndoCommand = new RelayCommand(
                obj => { _historyManager.Undo(); },
                obj => _historyManager.CanUndo
            );

            RedoCommand = new RelayCommand(
                obj => { _historyManager.Redo(); },
                obj => _historyManager.CanRedo
            );

            OpenDbAdminCommand = new RelayCommand(obj =>
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                    mainWindow.MainFrame.Navigate(new DatabaseAdminPage(_currentUser));
            });

            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, obj => SelectedProduct != null);
            DeleteCommand = new RelayCommand(ExecuteDelete, obj => SelectedProduct != null);

            LoadDataFromDbAsync();
        }

        private void ExecuteLogout(object obj)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainFrame.Navigate(new LoginPage());
            }
        }


        private async void LoadDataFromDbAsync()
        {
            var productsFromDb = await _repository.GetAllProductsAsync();

            _allProducts.Clear();
            foreach (var p in productsFromDb)
            {
                p.PropertyChanged += Product_PropertyChanged;
                _allProducts.Add(p);    
            }

            UpdateCategories();
            FilterProductsAsync();
        }

        private async void FilterProductsAsync()
        {
            var filtered = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var searchedFromDb = await _repository.SearchProductsAsync(SearchText);
                filtered = searchedFromDb;
            }

            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "Все категории")
            {
                filtered = filtered.Where(p => p.Category == SelectedCategory);
            }

            if (decimal.TryParse(MinPriceText, out decimal minPrice))
                filtered = filtered.Where(p => p.Price >= minPrice);

            if (decimal.TryParse(MaxPriceText, out decimal maxPrice))
                filtered = filtered.Where(p => p.Price <= maxPrice);

            DisplayProducts = new ObservableCollection<Product>(filtered);
        }

        private void UpdateCategories()
        {
            var oldCategory = SelectedCategory;
            Categories.Clear();
            Categories.Add("Все категории");

            var uniqueCategories = _allProducts.Select(p => p.Category).Distinct().OrderBy(c => c);
            foreach (var c in uniqueCategories)
            {
                if (!string.IsNullOrEmpty(c)) Categories.Add(c);
            }

            if (Categories.Contains(oldCategory))
                _selectedCategory = oldCategory;
            else
                _selectedCategory = "Все категории";

            OnPropertyChanged(nameof(SelectedCategory));
        }


        private void ExecuteAdd(object obj)
        {
            var addVM = new AddEditProductViewModel(null);
            var window = new AddEditProductWindow { DataContext = addVM };
            window.ShowDialog();

            if (addVM.DialogResult == true)
            {
                var newProduct = addVM.CurrentProduct;

                var action = new DelegateAction(
                    execute: async () =>
                    {
                        try
                        {
                            await _repository.AddProductAsync(newProduct);
                            newProduct.PropertyChanged += Product_PropertyChanged;
                            _allProducts.Add(newProduct);
                            UpdateCategories();
                            FilterProductsAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при добавлении в БД: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    },
                    undo: async () =>
                    {
                        try
                        {
                            await _repository.DeleteProductAsync(newProduct.Id);
                            _allProducts.Remove(newProduct);
                            UpdateCategories();
                            FilterProductsAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при отмене добавления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                );

                _historyManager.ExecuteAction(action);
            }
        }

        private void ExecuteEdit(object obj)
        {
            var oldProduct = new Product
            {
                Id = SelectedProduct.Id,
                Title = SelectedProduct.Title,
                Description = SelectedProduct.Description,
                Category = SelectedProduct.Category,
                Price = SelectedProduct.Price,
                Discount = SelectedProduct.Discount,
                StockQuantity = SelectedProduct.StockQuantity,
                MainImagePath = SelectedProduct.MainImagePath
            };

            var editVM = new AddEditProductViewModel(SelectedProduct);
            var window = new AddEditProductWindow { DataContext = editVM };
            window.ShowDialog();

            if (editVM.DialogResult == true)
            {
                var editedProduct = editVM.CurrentProduct;
                int index = _allProducts.IndexOf(SelectedProduct);

                var action = new DelegateAction(
                    execute: async () =>
                    {
                        await _repository.UpdateProductAsync(editedProduct);
                        _allProducts[index] = editedProduct;
                        UpdateCategories(); FilterProductsAsync();
                    },
                    undo: async () =>
                    {
                        await _repository.UpdateProductAsync(oldProduct);
                        _allProducts[index] = oldProduct;
                        UpdateCategories(); FilterProductsAsync();
                    }
                );

                _historyManager.ExecuteAction(action);
            }
        }

        private void ExecuteDelete(object obj)
        {
            var productToDelete = SelectedProduct;
            int index = _allProducts.IndexOf(productToDelete);

            var action = new DelegateAction(
                execute: async () =>
                {
                    await _repository.DeleteProductAsync(productToDelete.Id);
                    _allProducts.Remove(productToDelete);
                    UpdateCategories(); FilterProductsAsync();
                },
                undo: async () =>
                {
                    await _repository.AddProductAsync(productToDelete);
                    _allProducts.Insert(index, productToDelete);
                    UpdateCategories(); FilterProductsAsync();
                }
            );

            _historyManager.ExecuteAction(action);
        }

        private async void Product_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Rating" && sender is Product changedProduct)
            {
                try
                {
                    await _repository.UpdateProductAsync(changedProduct);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении рейтинга: {ex.Message}");
                }
            }
        }
    }
}