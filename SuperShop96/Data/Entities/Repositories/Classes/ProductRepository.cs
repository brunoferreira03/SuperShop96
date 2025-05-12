using Microsoft.EntityFrameworkCore;
using SuperShop96.Data.Entities;
using SuperShop96.Data.Entities.Repositories.Interfaces;
using System.Linq;

namespace SuperShop96.Data.Entities.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Products.Include(p => p.User);
        }
    }
}
