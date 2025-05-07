using Microsoft.AspNetCore.Identity;
using SuperShop96.Data.Entities;
using System.Threading.Tasks;

namespace SuperShop96.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
