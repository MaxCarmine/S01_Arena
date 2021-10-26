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

        [HttpPost]
        public async Task<string> StartArena() {
            var listOfFighters = await _repoFighters.GetList();
            var convertedFighters = ConvertEntityToModel(listOfFighters);
            _repoArena.Start(convertedFighters);
            return "Arena Has Begun";
        }

        private List<Fighter> ConvertEntityToModel(List<FighterEntity> listOfFighters) {
            var convertedFighters = new List<Fighter>();
            foreach(var fe in listOfFighters) {
                convertedFighters.Add(CloneFighters(fe));
            }
            return convertedFighters;
        }

        private Fighter CloneFighters(FighterEntity dbfighter) {
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
            { 1, typeof(Warrior) },
            { 2, typeof(Wizard) },
            { 3, typeof(Ork) }
        };

        [HttpGet]
        public List<string> RetriveLogs(int number) {
             return  _repoArena.GetLogs(number);
        }

        [HttpPost]
        public string StopGame() {
             _repoArena.Stop();
            return "The match has stoped";
        }
    }
}
