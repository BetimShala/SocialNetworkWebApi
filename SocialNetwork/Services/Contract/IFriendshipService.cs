using SocialNetwork.Helpers.Contract;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Contract
{
    public interface IFriendshipService : IServiceBase<FriendshipStatus>
    {
        
           IEnumerable<ConnectionsViewModel> GetNonFriendsList(string userId,string searchPhrase);
        IEnumerable<ConnectionsViewModel> GetFriendsList(string userId);
        IEnumerable<ConnectionsViewModel> GetSentFriendRequests(string userId);
        IEnumerable<ConnectionsViewModel> GetFriendRequests(string userId);

        bool UpdateFriendshipStatus(string userFromId, string userToId,int statusId);
    }
}
