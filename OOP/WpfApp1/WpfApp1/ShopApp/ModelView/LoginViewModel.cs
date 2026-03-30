using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Database;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.View;

namespace WpfApp1.ShopApp.ModelView
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserRepository _userRepository;

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
            _userRepository = new UserRepository();

            LoginCommand = new RelayCommand(ExecuteLogin, CanExecute);
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecute);
        }

        private bool CanExecute(object obj) => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        private async void ExecuteLogin(object obj)
        {
            ErrorMessage = string.Empty;

            User foundUser = await _userRepository.GetUserAsync(Username, Password);

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

        private async void ExecuteRegister(object obj)
        {
            ErrorMessage = string.Empty;

            bool exists = await _userRepository.UserExistsAsync(Username);
            if (exists)
            {
                ErrorMessage = "Пользователь с таким логином уже существует!";
                return;
            }

            User newUser = new User(Username, Password);
            await _userRepository.AddUserAsync(newUser);

            ErrorMessage = "Регистрация успешна! Теперь вы можете войти.";
            Password = string.Empty;
        }
    }
}