using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarigonaHill;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Services.Contract;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOptions<AppSettings> _appSettings;

        public AuthController(IAuthService authService,IOptions<AppSettings> appSettings)
        {
            _authService = authService;
            _appSettings = appSettings;
        }

        [HttpPost,Route("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            var user = _authService.Auth(model.Email, model.Password);

            if (user == null)
                return null;

            if (user.Result.Succeeded)
            {
                var loggedUser = _authService.GetLoggedUser(model.Email);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, loggedUser.Id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string retToken = tokenHandler.WriteToken(token);

                return Ok(new { token = retToken,userId = loggedUser.Id,fullName=loggedUser.FirstName+ " " + loggedUser.LastName});
            }
            return Ok(false);
        }

        [HttpPost,Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            var result = _authService.Register(model);
            return Ok(result.Result);
        }
    }
}