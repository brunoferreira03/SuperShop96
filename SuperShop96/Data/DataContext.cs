using Microsoft.EntityFrameworkCore;
using SuperShop96.Data.Entities;

namespace SuperShop96.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
