using Microsoft.AspNetCore.Identity;
using SuperShop96.Data;
using SuperShop96.Data.Entities;
using SuperShop96.Models;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SuperShop96.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
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

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
