using Microsoft.AspNetCore.Identity;
using SuperShop96.Data;
using SuperShop96.Data.Entities;
using System.Threading.Tasks;

namespace SuperShop96.Helpers
{
    public class UserHelper : IUserHelper
    {
        public UserManager<User> _userManager { get; }
        public UserHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
