using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly LoginRepository _repo;

        public LoginController(ILogger<LoginController> logger, LoginRepository repo) {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost]
        public async Task Login(string username) {
            if (_repo.UserExists(username)) {
                //GenerateToken and send it back
            } else {
                //Send string "User does not exist"
            }
        }
    }
}
