using SuperShop96.Data.Entities;
using SuperShop96.Data.Entities.Repositories.Interfaces;

namespace SuperShop96.Data.Entities.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
            
        }
    }
}
