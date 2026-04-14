using System;
using System.Threading.Tasks;
using WpfApp1.ShopApp.Model;

namespace WpfApp1.ShopApp.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }

        Task<int> SaveAsync();
    }
}