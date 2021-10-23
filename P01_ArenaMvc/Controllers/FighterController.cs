using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P01_ArenaMvc.DTO;
using P01_ArenaMvc.Entity;
using P01_ArenaMvc.Models;
using P01_ArenaMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FighterController  : ControllerBase
    {
        public FighterController(ILogger<FighterController> logger, FighterRepository repo) {
            _logger = logger;
            _repo = repo;
        }

        private readonly ILogger<FighterController> _logger;
        private readonly FighterRepository _repo;

        [HttpGet]
        public Task<List<FighterEntity>> Index() {
            return _repo.GetList();
        }

        [HttpPost]
        public async Task Add(FighterEntity fighter) {
            await _repo.AddFighter(fighter);
        }

        [HttpDelete]
        public async Task Delete(int id) {
            await _repo.DeleteFighter(id);
        }

        [HttpPut]
        public async Task Update(FighterDTO fighter, int id) {
            await _repo.UpdateFighter(fighter, id); 
        }

    }
}
