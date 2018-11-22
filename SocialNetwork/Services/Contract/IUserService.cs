using SocialNetwork.Helpers.Contract;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Contract
{
    public interface IUserService : IServiceBase<ApplicationUser>
    {
        IEnumerable<ConnectionsViewModel> GetNonFriendsList(string userId);
        IEnumerable<ConnectionsViewModel> GetFriendsList(string userId);
        bool DeleteFriendship(string userId, string deletedFriendUserId);
    }
}
