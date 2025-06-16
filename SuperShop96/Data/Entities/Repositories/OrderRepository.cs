using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SuperShop96.Data.Entities.Repositories.Classes;
using SuperShop96.Data.Entities.Repositories.Interfaces;
using SuperShop96.Helpers;
using SuperShop96.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace SuperShop96.Data.Entities.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string Username);

        Task<IQueryable<OrderDetailTemp>> GetDetailsTempAsync(string Username);

        Task AddItemToOrderAsync(AddItemViewModel model, string Username);

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

        Task DeleteDetailTempAsync(int id);
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

        public async Task AddItemToOrderAsync(AddItemViewModel model, string Username)
        {
            var user = await _userHelper.GetUserByEmailAsync(Username);

            if (user == null)
            {
                return;
            }

            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null)
            {
                return;
            }

            var orderDetailTemp = await _context.OrderDetailsTemp.Where(odt => odt.User == user && odt.Product == product).FirstOrDefaultAsync();

            if (orderDetailTemp == null)
            {
                orderDetailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user,
                };

                _context.OrderDetailsTemp.Add(orderDetailTemp);
            }
            else
            {
                orderDetailTemp.Quantity += model.Quantity;
                _context.OrderDetailsTemp.Update(orderDetailTemp);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null) { return; }

            _context.OrderDetailsTemp.Remove(orderDetailTemp);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<OrderDetailTemp>> GetDetailsTempAsync(string Username)
        {
            var user = await _userHelper.GetUserByEmailAsync(Username);

            if (user == null)
            {
                return null;
            }
            return _context.OrderDetailsTemp.
                Include(p => p.Product).
                Where(o => o.User == user).
                OrderBy(o => o.Product.Name);
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
                .Where(o => o.User == user)
                .OrderByDescending(o => o.OrderDate);
        }

        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            orderDetailTemp.Quantity += quantity;
            if (orderDetailTemp.Quantity > 0)
            {
                {
                    _context.OrderDetailsTemp.Update(orderDetailTemp);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
