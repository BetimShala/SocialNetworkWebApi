using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers
{
    public interface INotifyHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
