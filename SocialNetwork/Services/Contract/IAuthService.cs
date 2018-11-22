using Microsoft.AspNetCore.Identity;
using SocialNetwork.Helpers.Contract;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Contract
{
    public interface IAuthService : IServiceBase<ApplicationUser>
    {
        Task<SignInResult> Auth(string email, string password);

        Task<bool> Register(RegisterViewModel model);

        
        Task LogOutAsync();

        ApplicationUser GetLoggedUser(string email);

    }
}
