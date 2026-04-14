using System.Windows.Controls;
using WpfApp1.ShopApp.Model;
using WpfApp1.ShopApp.ModelView;
using WpfApp1.ShopApp.ViewModels;

namespace WpfApp1.ShopApp.View
{
    public partial class GamePage : Page
    {
        public GamePage(User user)
        {
            InitializeComponent();
            this.DataContext = new GameViewModel(user);
        }
    }
}