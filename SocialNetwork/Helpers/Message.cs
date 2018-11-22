using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers
{
    public class Message
    {
        public string Type { get; set; }
        public string Payload { get; set; }
        public string UserId { get; set; }
    }
}
