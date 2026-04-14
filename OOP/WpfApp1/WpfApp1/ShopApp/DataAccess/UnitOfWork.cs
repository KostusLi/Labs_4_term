using System;
using System.Threading.Tasks;
using WpfApp1.ShopApp.Database;
using WpfApp1.ShopApp.Model;

namespace WpfApp1.ShopApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDbContext _context;

        private IRepository<Product> _productRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;

        public UnitOfWork()
        {
            _context = new ShopDbContext();
        }

        public IRepository<Product> Products => _productRepository ??= new Repository<Product>(_context);
        public IRepository<Category> Categories => _categoryRepository ??= new Repository<Category>(_context);
        public IRepository<User> Users => _userRepository ??= new Repository<User>(_context);
        public IRepository<Role> Roles => _roleRepository ??= new Repository<Role>(_context);


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}