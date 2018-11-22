using Microsoft.EntityFrameworkCore;
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
    public class UserService : ServiceBase<ApplicationUser>, IUserService
    {
        public UserService(DataContext context) : base(context)
        {
        }

        public bool DeleteFriendship(string userId, string deletedFriendUserId)
        {
            return false;
        }

        public IEnumerable<ConnectionsViewModel> GetFriendsList(string userId)
        {
            var friends = _context.FriendshipStatuses.Include(x => x.AddresseeUser).Include(x=>x.RequesterUser)
                                                     .Where(x=>x.StatusId==2 && (x.AddresseeUserId.Equals(userId) || x.RequesterUserId.Equals(userId))).ToList();
            var model = new List<ConnectionsViewModel>();
            foreach(var friend in friends)
            {
                var temp = friend.AddresseeUser.Id.Equals(userId) ? friend.RequesterUser : friend.AddresseeUser;
                model.Add(new ConnectionsViewModel
                {
                    Id = temp.Id,
                    FirstName = temp.FirstName,
                    LastName = temp.LastName
                });
            }
            return model;
            
        }

        public IEnumerable<ConnectionsViewModel> GetNonFriendsList(string userId)
        {
            var users = _ctx.Include(a => a.SentFriendRequests)
                             .Include(a => a.ReceievedFriendRequests).Where(a=>!a.Id.Equals(userId));
            var result = new List<ConnectionsViewModel>();
            foreach(var user in users)
            {
                if(user.SentFriendRequests.Count==0 && user.ReceievedFriendRequests.Count == 0)
                {
                    result.Add(new ConnectionsViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    });
                }
                else
                {
                    if(user.ReceievedFriendRequests.Any(a=>(a.AddresseeUserId.Equals(userId) || a.RequesterUserId.Equals(userId))&& a.StatusId!=4) || 
                        user.SentFriendRequests.Any(a =>( a.AddresseeUserId.Equals(userId) || a.RequesterUserId.Equals(userId))  && a.StatusId != 4))
                    {

                    }
                    else
                    {
                        result.Add(new ConnectionsViewModel
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        });
                    }
                }
            }
            return result;
        }
    }
}
