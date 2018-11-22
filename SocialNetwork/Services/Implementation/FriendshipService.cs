using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Helpers.Implementation;
using SocialNetwork.Models;
using SocialNetwork.Services.Contract;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Implementation
{
    public class FriendshipService : ServiceBase<FriendshipStatus>, IFriendshipService
    {
        public FriendshipService(DataContext context) : base(context)
        {
        }

        public IEnumerable<ConnectionsViewModel> GetNonFriendsList(string userId,string searchPhrase)
        {
            var model = new List<ConnectionsViewModel>();

            using (var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                var cmd = new SqlCommand("[dbo].[GetNonFriendsList]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurrentUserId", userId);
                cmd.Parameters.AddWithValue("@SearchPhrase", searchPhrase);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new ConnectionsViewModel();
                        p.Id = reader["Id"].ToString();
                        p.FirstName = reader["FirstName"].ToString();
                        p.LastName = reader["LastName"].ToString();
                        model.Add(p);

                    }
                }
            }
            return model;            
        }

        public IEnumerable<ConnectionsViewModel> GetFriendsList(string userId)
        {
            var friends = _ctx.Include(x => x.AddresseeUser).Include(x => x.RequesterUser)
                                                     .Where(x => x.StatusId == 2 && (x.AddresseeUserId.Equals(userId) || x.RequesterUserId.Equals(userId))).ToList();
            var model = new List<ConnectionsViewModel>();
            foreach (var friend in friends)
            {
                var temp = friend.AddresseeUser.Id.Equals(userId) ? friend.RequesterUser : friend.AddresseeUser;
                model.Add(new ConnectionsViewModel
                {
                    Id = temp.Id,
                    FirstName = temp.FirstName,
                    LastName = temp.LastName,
                    Status = friend.StatusId
                });
            }
            return model;

        }

        public bool UpdateFriendshipStatus(string userFromId, string userToId, int statusId)
        {
            return true;
        }

        public IEnumerable<ConnectionsViewModel> GetSentFriendRequests(string userId)
        {
            var friends = _ctx.Include(x => x.AddresseeUser)
                                                    .Where(x => x.StatusId == 3 && x.RequesterUserId.Equals(userId)).ToList();
            var model = new List<ConnectionsViewModel>();
            foreach (var friend in friends)
            {
                model.Add(new ConnectionsViewModel
                {
                    Id = friend.AddresseeUser.Id,
                    FirstName = friend.AddresseeUser.FirstName,
                    LastName = friend.AddresseeUser.LastName,
                    Status = friend.StatusId
                });
            }
            return model;
        }

        public IEnumerable<ConnectionsViewModel> GetFriendRequests(string userId)
        {
            var friends = _ctx.Include(x => x.RequesterUser)
                                                    .Where(x => x.StatusId == 3 && x.AddresseeUserId.Equals(userId)).ToList();
            var model = new List<ConnectionsViewModel>();
            foreach (var friend in friends)
            {
                model.Add(new ConnectionsViewModel
                {
                    Id = friend.RequesterUser.Id,
                    FirstName = friend.RequesterUser.FirstName,
                    LastName = friend.RequesterUser.LastName,
                    Status = friend.StatusId
                });
            }
            return model;
        }
    }
}
