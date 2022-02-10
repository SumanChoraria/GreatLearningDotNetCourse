using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SumanChoraria_Sprint1.Models;
using SumanChoraria_Sprint1.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumanChoraria_Sprint1.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : LoginBaseController
    {
        private IConfiguration _config;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="baseRepository"></param>
        public AuthenticateController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;

        }
        private string GenerateJSONWebToken(LoginModel userInfo)
        {
            if (userInfo != null && !userInfo.UserName.IsNullOrEmpty())
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private async Task<LoginModel> AuthenticateUser(LoginModel login)
        {
            LoginModel user = null;

            if (login.UserName == "admin")
            {
                user = new LoginModel { UserName = "admin", Password = "admin" };
            }
            else
            {
                var User = _userRepository.GetAll().FirstOrDefault(u => u.FirstName == login.UserName && u.Password == login.Password);
                if (User == null)
                {
                    return null;
                }
                user = new LoginModel { UserName = login.UserName, Password = login.Password };
            }
            return user;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel data)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(data);
            //For demo purpose allowing any username and password
            if (data != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                if (!tokenString.IsNullOrEmpty())
                {
                    response = Ok(new { CurrentUser = user.UserName, Token = tokenString, Message = "Success" });
                }
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(Get))]
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return new string[] { accessToken };
        }

    }
}
