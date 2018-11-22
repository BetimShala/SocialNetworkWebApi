using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialNetwork.Models;
using SocialNetwork.Services.Contract;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IFriendshipService _friendshipService;
        private readonly IAuthService _authService;
        public UsersController(IUserService userService,IAuthService authService, IFriendshipService friendshipService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
            _friendshipService = friendshipService;
            _authService = authService;
        }
        
        [HttpGet,Route("populate")]
        public IActionResult PopulateFromJson()
        {
            var filepath = @"wwwroot\users-dummy.json";
            var JSON = System.IO.File.ReadAllText(filepath);

            var obj = JsonConvert.DeserializeObject(JSON);
            var model = new RegisterViewModel();
            var stringChars = new char[8];
            var random = new Random();

            foreach (var item in ((JArray)obj))
            {               
                model.FirstName = item.Value<string>("firstName");
                model.LastName = item.Value<string>("lastName");
                model.Password = "P@ssw0rd";
                model.Email = model.FirstName +"_" + model.LastName + "@test.com";

                _authService.Register(model);

                System.Threading.Thread.Sleep(500);

            }

            return Ok();
        }
    }
}