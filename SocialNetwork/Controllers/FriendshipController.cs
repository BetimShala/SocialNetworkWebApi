using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Models;
using SocialNetwork.Services.Contract;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet, Route("nonfriends/{userId}")]
        public IActionResult GetAll([FromRoute] string userId,string searchPhrase)
        {
            var users = _friendshipService.GetNonFriendsList(userId,searchPhrase);
            return Ok(JsonConvert.SerializeObject(users,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [HttpGet, Route("friends/{userId}")]
        public IActionResult GetFriendsList([FromRoute] string userId)
       {
            var friendsList = _friendshipService.GetFriendsList(userId).OrderByDescending(f=>f.Id);
            return Ok(JsonConvert.SerializeObject(friendsList,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [HttpGet, Route("sentFriendRequests/{userId}")]
        public IActionResult GetSentFriendRequests([FromRoute] string userId)
        {
            var sentRequests = _friendshipService.GetSentFriendRequests(userId);
            return Ok(JsonConvert.SerializeObject(sentRequests,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [HttpGet, Route("getFriendRequests/{userId}")]
        public IActionResult GetFriendRequests([FromRoute] string userId)
        {
            var sentRequests = _friendshipService.GetFriendRequests(userId);
            return Ok(JsonConvert.SerializeObject(sentRequests,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [HttpPut, Route("updateFriendship")]
        public IActionResult UpdateFriendship(UpdateFriendshipViewModel model)
        {

            var friendship = _friendshipService.GetWhere(f => (f.AddresseeUserId.Equals(model.UserFromId)  || 
                                                               f.RequesterUserId.Equals(model.UserFromId)) &&
                                                               (f.AddresseeUserId.Equals(model.UserToId)   || 
                                                               f.RequesterUserId.Equals(model.UserToId))).FirstOrDefault();
            if (friendship == null)
            {
                _friendshipService.Add(new FriendshipStatus
                {
                    RequesterUserId = model.UserFromId,
                    AddresseeUserId = model.UserToId,
                    StatusId = model.StatusId
                });
            }
            else
            {
                friendship.RequesterUserId = model.UserFromId;
                friendship.AddresseeUserId = model.UserToId;
                friendship.StatusId = model.StatusId;
                _friendshipService.Update(friendship);
            }

            return Ok(true);
        }

        [HttpDelete, Route("removeFriendship/{userFromId}/{userToId}")]
        public IActionResult RemoveFriendship([FromRoute] string userFromId,[FromRoute] string userToId)
        {

            var friendship = _friendshipService.GetWhere(f => (f.AddresseeUserId.Equals(userFromId) ||
                                                               f.RequesterUserId.Equals(userFromId)) &&
                                                               (f.AddresseeUserId.Equals(userToId) ||
                                                               f.RequesterUserId.Equals(userToId))).FirstOrDefault();

            if (friendship == null)
                return Ok(false);
            else
                _friendshipService.Remove(friendship);

            return Ok(true);
        }
    }
}