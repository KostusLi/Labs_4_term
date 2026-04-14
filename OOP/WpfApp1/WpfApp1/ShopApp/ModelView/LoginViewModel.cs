using System.Linq;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.DataAccess; // Подключаем наш UnitOfWork
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;
using WpfApp1.ShopApp.View;

namespace WpfApp1.ShopApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private string _username;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }

        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }

        private string _errorMessage;
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(); } }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public LoginViewModel()
        {
            _unitOfWork = new UnitOfWork();

            LoginCommand = new RelayCommand(ExecuteLogin, CanExecute);
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecute);
        }

        private bool CanExecute(object obj) => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);


        private async void ExecuteLogin(object obj)
        {
            ErrorMessage = string.Empty;

            try
            {
                var users = await _unitOfWork.Users.FindAsync(
                    u => u.Username == Username && u.Password == Password,
                    u => u.Role
                );

                User foundUser = users.FirstOrDefault();

                if (foundUser != null)
                {
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        mainWindow.MainFrame.Navigate(new ProductsPage(foundUser));
                    }
                }
                else
                {
                    ErrorMessage = "Неверный логин или пароль!";
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"Ошибка БД: {ex.Message}";
            }
        }


        private async void ExecuteRegister(object obj)
        {
            ErrorMessage = string.Empty;

            try
            {
                var existingUsers = await _unitOfWork.Users.FindAsync(u => u.Username == Username);
                if (existingUsers.Any())
                {
                    ErrorMessage = "Пользователь с таким логином уже существует!";
                    return;
                }

                User newUser = new User { Username = Username, Password = Password, RoleId = 2 };

                await _unitOfWork.Users.AddAsync(newUser);

                await _unitOfWork.SaveAsync();

                ErrorMessage = "Регистрация успешна! Теперь вы можете войти.";
                Password = string.Empty;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"Ошибка БД: {ex.Message}";
            }
        }
    }
}