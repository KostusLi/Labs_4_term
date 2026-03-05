using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ViewModels;

namespace WpfApp1.ShopApp.View
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage(User currentUser)
        {
            InitializeComponent();
            this.DataContext = new ProfileViewModel(currentUser);
        }
    }
}
