using Microsoft.AspNetCore.Identity;
using SocialNetwork.Data;
using SocialNetwork.Helpers.Implementation;
using SocialNetwork.Models;
using SocialNetwork.Services.Contract;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Implementation
{
    public class AuthService : ServiceBase<ApplicationUser>, IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager, 
                           DataContext context) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<SignInResult> Auth(string email, string password)
        {
            return _signInManager.PasswordSignInAsync(email, password,false,false);
        }

        public ApplicationUser GetLoggedUser(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }

        public Task LogOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }

            return false;

        }

       
    }
}
