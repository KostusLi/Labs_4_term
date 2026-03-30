using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;
using WpfApp1.ShopApp.View;

namespace WpfApp1.ShopApp.ModelView
{
    public class DatabaseAdminViewModel : BaseViewModel
    {
        private readonly User _currentUser;
        private readonly string _connectionString;

        private DataTable _currentTable;

        public DataView CurrentTableData => _currentTable?.DefaultView;

        public RelayCommand LoadUsersCommand { get; }
        public RelayCommand LoadProductsCommand { get; }
        public RelayCommand LoadCategoriesCommand { get; }
        public RelayCommand LoadRolesCommand { get; }
        public RelayCommand GoBackCommand { get; }

        public DatabaseAdminViewModel(User user)
        {
            _currentUser = user;
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            LoadUsersCommand = new RelayCommand(obj => LoadTableData("Users"));
            LoadProductsCommand = new RelayCommand(obj => LoadTableData("Products"));
            LoadCategoriesCommand = new RelayCommand(obj => LoadTableData("Categories"));
            LoadRolesCommand = new RelayCommand(obj => LoadTableData("Roles"));

            GoBackCommand = new RelayCommand(obj =>
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                    mainWindow.MainFrame.Navigate(new ProductsPage(_currentUser));
            });

            LoadTableData("Users");
        }

        private async void LoadTableData(string tableName)
        {
            await Task.Run(() =>
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string sql = $"SELECT * FROM {tableName}";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable(tableName);
                        adapter.Fill(dt);

                        _currentTable = dt;

                        OnPropertyChanged(nameof(CurrentTableData));
                    }
                }
            });
        }
    }
}