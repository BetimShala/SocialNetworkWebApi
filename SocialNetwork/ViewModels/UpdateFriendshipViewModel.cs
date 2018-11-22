using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.ViewModels
{
    public class UpdateFriendshipViewModel
    {
        public string UserFromId { get; set; }
        public string UserToId { get; set; }
        public int StatusId { get; set; }
    }
}
