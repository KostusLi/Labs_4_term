using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.View;
using WpfApp1.ShopApp.ViewModels;

namespace WpfApp1.ShopApp.ModelView
{
    public class LoginViewModel : BaseViewModel
    {
        private List<User> _users;

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public LoginViewModel()
        {
            _users = new List<User>
            {
                new User()
            };

            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
        }

        private bool CanExecuteLogin(object obj)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object obj)
        {
            ErrorMessage = string.Empty;

            var foundUser = _users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (foundUser != null)
            {
                var productsPage = new ProductsPage(foundUser);

                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MainFrame.Navigate(productsPage);
                }
            }
            else
            {
                ErrorMessage = "Неверный логин или пароль!";
            }
        }

        private bool CanExecuteRegister(object obj)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteRegister(object obj)
        {
            ErrorMessage = string.Empty;

            if (_users.Any(u => u.Username == Username))
            {
                ErrorMessage = "Пользователь с таким логином уже существует!";
                return;
            }

            User newUser = new User(Username, Password);
            _users.Add(newUser);

            ErrorMessage = "Регистрация успешна! Теперь вы можете войти.";

            Password = string.Empty;
        }

    }
}
