using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P01_ArenaMvc.Models.Attributes;
using P01_ArenaMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginRepository _repoLogin;

        public LoginController(ILogger<LoginController> logger, LoginRepository repo) {
            _repoLogin = repo;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymouse]
        public async Task<string> Login(string username) {
            if (_repoLogin.UserExists(username)) {
                var t =  _repoLogin.GetToken(username);
                return t;
            } else {
                //Send string "User does not exist"
                return $"invalid user {username} does not exist";
            }
        }
    }
}
