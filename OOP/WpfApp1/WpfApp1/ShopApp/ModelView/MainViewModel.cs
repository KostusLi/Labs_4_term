using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.DataAccess;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;
using WpfApp1.ShopApp.UndoRedo;
using WpfApp1.ShopApp.View;

namespace WpfApp1.ShopApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly IUnitOfWork _unitOfWork;
        private readonly UndoRedoManager _historyManager;

        private ObservableCollection<Product> _allProducts;
        private ObservableCollection<Product> _displayProducts;

        public RelayCommand SwitchToRuCommand { get; }
        public RelayCommand SwitchToEnCommand { get; }
        public RelayCommand OpenProfileCommand { get; }
        public RelayCommand OpenDbAdminCommand { get; }
        public RelayCommand OpenGameCommand { get; }
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public string CurrentUsername => _currentUser?.Username;
        public bool IsAdmin => _currentUser?.Role?.Name == "Admin";

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
        public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); FilterProductsAsync(); } }

        private string _selectedCategory;
        public string SelectedCategory { get => _selectedCategory; set { _selectedCategory = value; OnPropertyChanged(); FilterProductsAsync(); } }

        private string _minPriceText;
        public string MinPriceText { get => _minPriceText; set { _minPriceText = value; OnPropertyChanged(); FilterProductsAsync(); } }

        private string _maxPriceText;
        public string MaxPriceText { get => _maxPriceText; set { _maxPriceText = value; OnPropertyChanged(); FilterProductsAsync(); } }

        public MainViewModel(User user)
        {
            _currentUser = user;
            _unitOfWork = new UnitOfWork();
            _historyManager = new UndoRedoManager();

            _allProducts = new ObservableCollection<Product>();
            DisplayProducts = new ObservableCollection<Product>();
            Categories = new ObservableCollection<string>();

            SwitchToRuCommand = new RelayCommand(obj => App.ChangeLanguage("ru"));
            SwitchToEnCommand = new RelayCommand(obj => App.ChangeLanguage("en"));
            LogoutCommand = new RelayCommand(obj => { if (Application.Current.MainWindow is MainWindow mw) mw.MainFrame.Navigate(new LoginPage()); });
            OpenProfileCommand = new RelayCommand(obj => { if (Application.Current.MainWindow is MainWindow mw) mw.MainFrame.Navigate(new ProfilePage(_currentUser)); });

            UndoCommand = new RelayCommand(obj => { _historyManager.Undo(); }, obj => _historyManager.CanUndo);
            RedoCommand = new RelayCommand(obj => { _historyManager.Redo(); }, obj => _historyManager.CanRedo);

            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, obj => SelectedProduct != null);
            DeleteCommand = new RelayCommand(ExecuteDelete, obj => SelectedProduct != null);

            OpenDbAdminCommand = new RelayCommand(obj =>
            {
                if (Application.Current.MainWindow is MainWindow mw)
                    mw.MainFrame.Navigate(new DatabaseAdminPage(_currentUser));
            });

            OpenGameCommand = new RelayCommand(obj =>
            {
                if (Application.Current.MainWindow is MainWindow mw)
                    mw.MainFrame.Navigate(new GamePage(_currentUser));
            });
            LoadDataFromDbAsync();
        }


        private async void LoadDataFromDbAsync()
        {
            try
            {
                var productsFromDb = await _unitOfWork.Products.FindAsync(p => true, p => p.Category);

                _allProducts.Clear();
                foreach (var p in productsFromDb)
                {
                    p.PropertyChanged += Product_PropertyChanged;
                    _allProducts.Add(p);
                }

                UpdateCategories();
                FilterProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }


        private void FilterProductsAsync()
        {
            var filtered = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(p => p.Title.ToLower().Contains(SearchText.ToLower()));

            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "Все категории")
                filtered = filtered.Where(p => p.Category?.Name == SelectedCategory);

            if (decimal.TryParse(MinPriceText, out decimal minPrice))
                filtered = filtered.Where(p => p.Price >= minPrice);

            if (decimal.TryParse(MaxPriceText, out decimal maxPrice))
                filtered = filtered.Where(p => p.Price <= maxPrice);

            filtered = filtered.OrderBy(p => p.Price);

            DisplayProducts = new ObservableCollection<Product>(filtered);
        }

        private void UpdateCategories()
        {
            var oldCategory = SelectedCategory;
            Categories.Clear();
            Categories.Add("Все категории");

            var uniqueCategories = _allProducts.Where(p => p.Category != null).Select(p => p.Category.Name).Distinct().OrderBy(c => c);
            foreach (var c in uniqueCategories)
                Categories.Add(c);

            _selectedCategory = Categories.Contains(oldCategory) ? oldCategory : "Все категории";
            OnPropertyChanged(nameof(SelectedCategory));
        }


        private async void ExecuteAdd(object obj)
        {
            var categoriesDb = (await _unitOfWork.Categories.GetAllAsync()).ToList();

            var addVM = new AddEditProductViewModel(null, categoriesDb);
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
                            await _unitOfWork.Products.AddAsync(newProduct);
                            await _unitOfWork.SaveAsync();

                            newProduct.PropertyChanged += Product_PropertyChanged;
                            _allProducts.Add(newProduct);
                            UpdateCategories(); FilterProductsAsync();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    },
                    undo: async () =>
                    {
                        try
                        {
                            _unitOfWork.Products.Delete(newProduct);
                            await _unitOfWork.SaveAsync();

                            _allProducts.Remove(newProduct);
                            UpdateCategories(); FilterProductsAsync();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                );

                _historyManager.ExecuteAction(action);
            }
        }

        private async void ExecuteEdit(object obj)
        {
            var categoriesDb = (await _unitOfWork.Categories.GetAllAsync()).ToList();

            var oldProduct = new Product { Id = SelectedProduct.Id, Title = SelectedProduct.Title, Price = SelectedProduct.Price, CategoryId = SelectedProduct.CategoryId, ImageData = SelectedProduct.ImageData, StockQuantity = SelectedProduct.StockQuantity, Discount = SelectedProduct.Discount, Description = SelectedProduct.Description };

            var editVM = new AddEditProductViewModel(SelectedProduct, categoriesDb);
            var window = new AddEditProductWindow { DataContext = editVM };
            window.ShowDialog();

            if (editVM.DialogResult == true)
            {
                var editedProduct = editVM.CurrentProduct;
                int index = _allProducts.IndexOf(SelectedProduct);

                var trackedProduct = _allProducts[index];

                var action = new DelegateAction(
                    execute: async () =>
                    {
                        try
                        {
                            trackedProduct.Title = editedProduct.Title;
                            trackedProduct.Description = editedProduct.Description;
                            trackedProduct.CategoryId = editedProduct.CategoryId;
                            trackedProduct.Price = editedProduct.Price;
                            trackedProduct.Discount = editedProduct.Discount;
                            trackedProduct.StockQuantity = editedProduct.StockQuantity;
                            trackedProduct.ImageData = editedProduct.ImageData;

                            await _unitOfWork.SaveAsync();

                            UpdateCategories(); FilterProductsAsync();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    },
                    undo: async () =>
                    {
                        try
                        {
                            trackedProduct.Title = oldProduct.Title;
                            trackedProduct.CategoryId = oldProduct.CategoryId;
                            trackedProduct.Price = oldProduct.Price;
                            trackedProduct.Discount = oldProduct.Discount;
                            trackedProduct.StockQuantity = oldProduct.StockQuantity;
                            trackedProduct.ImageData = oldProduct.ImageData;
                            trackedProduct.Description = oldProduct.Description;

                            await _unitOfWork.SaveAsync();

                            UpdateCategories(); FilterProductsAsync();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                    try
                    {
                        _unitOfWork.Products.Delete(productToDelete);
                        await _unitOfWork.SaveAsync();
                        _allProducts.Remove(productToDelete);
                        UpdateCategories(); FilterProductsAsync();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                },
                undo: async () =>
                {
                    try
                    {
                        await _unitOfWork.Products.AddAsync(productToDelete);
                        await _unitOfWork.SaveAsync();
                        _allProducts.Insert(index, productToDelete);
                        UpdateCategories(); FilterProductsAsync();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                    _unitOfWork.Products.Update(changedProduct);
                    await _unitOfWork.SaveAsync();
                }
                catch { }
            }
        }
    }
}