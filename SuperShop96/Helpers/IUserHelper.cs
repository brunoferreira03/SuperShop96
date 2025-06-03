using Microsoft.AspNetCore.Identity;
using SuperShop96.Data.Entities;
using SuperShop96.Models;
using System.Threading.Tasks;

namespace SuperShop96.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
    }
}
