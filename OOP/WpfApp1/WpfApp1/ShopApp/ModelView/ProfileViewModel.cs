using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.DataAccess;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;
using WpfApp1.ShopApp.View;

namespace WpfApp1.ShopApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly IUnitOfWork _unitOfWork;

        private string _editUsername;
        public string EditUsername
        {
            get => _editUsername;
            set { _editUsername = value; OnPropertyChanged(); }
        }

        private string _editPassword;
        public string EditPassword
        {
            get => _editPassword;
            set { _editPassword = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveProfileCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public RelayCommand ChangeThemeCommand { get; }
        public RelayCommand ChangeLangCommand { get; }

        public ProfileViewModel(User user)
        {
            _currentUser = user;

            _unitOfWork = new UnitOfWork();

            EditUsername = user.Username;
            EditPassword = user.Password;

            SaveProfileCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            GoBackCommand = new RelayCommand(ExecuteGoBack);
            ChangeThemeCommand = new RelayCommand(obj => App.ChangeTheme(obj.ToString()));
            ChangeLangCommand = new RelayCommand(obj => App.ChangeLanguage(obj.ToString()));
        }

        private bool CanExecuteSave(object obj)
        {
            return !string.IsNullOrWhiteSpace(EditUsername) && !string.IsNullOrWhiteSpace(EditPassword);
        }

        private async void ExecuteSave(object obj)
        {
            _currentUser.Username = EditUsername;
            _currentUser.Password = EditPassword;

            _unitOfWork.Users.Update(_currentUser);
            await _unitOfWork.SaveAsync();

            MessageBox.Show("Данные профиля успешно обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteGoBack(object obj)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainFrame.Navigate(new ProductsPage(_currentUser));
            }
        }
    }
}