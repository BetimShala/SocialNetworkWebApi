using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class FriendshipStatus
    {
        public int Id { get; set; }

        public ApplicationUser RequesterUser { get; set; }
        public ApplicationUser AddresseeUser { get; set; }

        public string RequesterUserId { get; set; }
        public string AddresseeUserId { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        
    }
}
