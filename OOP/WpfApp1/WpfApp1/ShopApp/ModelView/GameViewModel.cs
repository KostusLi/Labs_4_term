using System;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp1.ShopApp.Commands;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;
using WpfApp1.ShopApp.View;

namespace WpfApp1.ShopApp.ModelView
{
    public class GameViewModel : BaseViewModel
    {
        private User _currentUser;
        public ObservableCollection<DroneViewModel> Drones { get; set; }

        public RelayCommand AddDroneCommand { get; }
        public RelayCommand ClearDronesCommand { get; }
        public RelayCommand GoBackCommand { get; }

        private Random _rnd = new Random();

        public GameViewModel(User currentUser)
        {
            _currentUser = currentUser;
            Drones = new ObservableCollection<DroneViewModel>();

            AddDroneCommand = new RelayCommand(obj =>
            {
                string name = $"Дрон-{Drones.Count + 1}";
                Drones.Add(new DroneViewModel(name, _rnd.Next(50, 400), _rnd.Next(50, 300)));
            });

            ClearDronesCommand = new RelayCommand(obj => Drones.Clear());

            GoBackCommand = new RelayCommand(obj =>
            {
                if (Application.Current.MainWindow is MainWindow mw)
                    mw.MainFrame.Navigate(new ProductsPage(_currentUser));
            });
        }
    }
}