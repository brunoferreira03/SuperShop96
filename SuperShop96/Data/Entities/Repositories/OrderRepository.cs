using Microsoft.EntityFrameworkCore;
using SuperShop96.Data.Entities.Repositories.Classes;
using SuperShop96.Data.Entities.Repositories.Interfaces;
using SuperShop96.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop96.Data.Entities.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string Username);
    }

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string Username)
        {
            var user = await _userHelper.GetUserByEmailAsync(Username);

            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Orders
                    .Include(o => o.Items) // Include connects table A to table B
                    .ThenInclude(i => i.Product) // Then Include connects table A to table C, given that table C is connected to table B
                    .OrderByDescending(o => o.OrderDate);
            }

            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .Where( o => o.User == user)
                .OrderByDescending (o => o.OrderDate);
        }
    }
}
