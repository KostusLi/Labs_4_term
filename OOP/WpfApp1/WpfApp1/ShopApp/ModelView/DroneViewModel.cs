using System;
using WpfApp1.ShopApp.Commands;

namespace WpfApp1.ShopApp.ModelView
{
    public class DroneViewModel : BaseViewModel
    {
        private double _x;
        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }

        private double _y;
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }

        private double _angle;
        public double Angle { get => _angle; set { _angle = value; OnPropertyChanged(); } }

        private double _size;
        public double Size { get => _size; set { _size = value; OnPropertyChanged(); } }

        private string _droneColor;
        public string DroneColor { get => _droneColor; set { _droneColor = value; OnPropertyChanged(); } }

        public string Name { get; set; }

        public RelayCommand MoveForwardCommand { get; }
        public RelayCommand RotateCommand { get; }
        public RelayCommand UpgradeCommand { get; }

        public DroneViewModel(string name, double startX, double startY)
        {
            Name = name;
            X = startX;
            Y = startY;
            Angle = 0;

            Size = 60;

            DroneColor = "#2196F3";

            MoveForwardCommand = new RelayCommand(obj => MoveForward());
            RotateCommand = new RelayCommand(obj => { Angle += 45; });

            UpgradeCommand = new RelayCommand(obj =>
            {
                Size += 15;
                DroneColor = "#FFC107";
            });
        }

        private void MoveForward()
        {
            double radians = Angle * (Math.PI / 180.0);
            X += Math.Cos(radians) * 20;
            Y += Math.Sin(radians) * 20;
        }
    }
}