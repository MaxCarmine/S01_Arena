using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P01_ArenaMvc.Entity;
using P01_ArenaMvc.Models;
using P01_ArenaMvc.Models.Classes.Fighters.Fighters.Type;
using P01_ArenaMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class ArenaController : ControllerBase
    {
        private readonly ILogger<ArenaController> _logger;
        private readonly ArenaRepository _repoArena;
        private readonly FighterRepository _repoFighters;

        public ArenaController(ILogger<ArenaController> logger, ArenaRepository repoArena, FighterRepository repoFighter) {
            _repoArena = repoArena;
            _repoFighters = repoFighter;
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> StartArena() {
            var listOfFighters = await _repoFighters.GetList();
            var convertedFighters = ConvertEntityToModal(listOfFighters);
             _repoArena.Start(convertedFighters);
            return "Arena Has Begun";
        }

        private  List<Fighter> ConvertEntityToModal(List<FighterEntity> listOfFighters) {
            var convertedFighters = new List<Fighter>();
            foreach(var fe in listOfFighters) {
                convertedFighters.Add(CloneFighters(fe));
            }
            return convertedFighters;
        }

        private  Fighter CloneFighters(FighterEntity dbfighter) {
            var FightersDictionary = FD;
            var fighterClass = dbfighter.Class;
            var fighterType = FightersDictionary[fighterClass];
            var istance = (Fighter)Activator.CreateInstance(fighterType);
            istance.ID = dbfighter.Id;
            istance.EpicName = dbfighter.EpicName;
            istance.Class = fighterClass;
            istance.PV = dbfighter.PV;
            istance.Speed = dbfighter.Speed;
            istance.Damage = (int)dbfighter.Damage;
            return istance;
        }

        private Dictionary<int, Type> FD = new Dictionary<int, Type> {
            {1, typeof(Warrior) },
            {2, typeof(Wizard) },
            {3, typeof(Ork) },
            //{4, typeof(Assasin) },
            //{5, typeof(Healer) },
        };


        [HttpGet]
        public  async Task<List<string>> RetriveLogs(int number) {
             return await _repoArena.GetLogs(number);
        }


        [HttpGet]
        public async Task<string> StopGame() {
             _repoArena.Stop();
            return "The match has stoped";
        }
    }
}
