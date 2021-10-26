using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace P01_ArenaMvc.Models.Classes.Arena
{
    public class Arena
    {
        //Get Fighters
        public List<Fighter> Fighters { get; set; } 
        public event Action<Fighter> HasWon;
        public int _Turn { get; set; }
        public int _Round { get; set; }
        public bool GameInProgress { get; set; }
        const string msg = "Arena has begun";
        public OutputManager outputManager;

        public Arena(List<Fighter> fighters) {
            outputManager = new OutputManager();
            Fighters = fighters;
            AddEvent();

        }

        public List<string> GetLogs(int numberOfLogs) {
            outputManager.Logs.Add(msg);
            if (numberOfLogs >= 1) {
                var lenthg = outputManager.Logs.Count();
                var index = lenthg - numberOfLogs;
                return outputManager.Logs.Skip(index).ToList();
            }
            return outputManager.Logs;
        }

        private void AddEvent() {
            foreach(var f in Fighters) {
                f.Attacked += outputManager.Attack;
                f.Damaged += outputManager.Hit;
                f.Died += outputManager.Died;
            }
            HasWon += outputManager.Won;
        }

        public  void StopGame() {
             GameInProgress = false;
        }

        public async void StartGame() {
            _Turn = 0;
            _Round = 1;
            GameInProgress = true;
            var OrderOfAttack = SortFighters(Fighters);
            var deadPool = Fighters;
            var moreThanOneFighterIsAlive = deadPool.Count > 1;
                while (moreThanOneFighterIsAlive && GameInProgress) {
                if (OneFighterLeft(deadPool)) {
                    continue;
                }
                StartOfRound(OrderOfAttack, deadPool);
                await Task.Delay(1000);
                EndOfRound(OrderOfAttack, deadPool);
            }
        }

        private void Winner(List<Fighter> f) {
            var winner = f[0];
            FighterHasWon(winner);
        }
        private void FighterHasWon(Fighter winner) {
            HasWon?.Invoke(winner);
        }

        private async void StartOfRound(List<Fighter> attackOrder, List<Fighter> deadpool) {
            foreach (var f in attackOrder) {
                if (IsFighterAlive(f, deadpool)) {
                    await Task.Delay(500);
                    var adv = PickAdversire(f, deadpool);
                    AttackersTurn(f, adv);
                } else {
                    continue;
                }
            }
        }

        private bool OneFighterLeft(List<Fighter> deadpool) {
            if (deadpool.Count() == 1) {
                Winner(deadpool);
                return true;
            }
            return false;
        }

        private void AttackersTurn(Fighter attacker, Fighter advesire) {
            attacker.Fight(advesire);
            _Turn++;
        }

        static private Fighter PickAdversire(Fighter currFighter, List<Fighter> deadpool) {
            var chooseFighter = true;
            var fCount = deadpool.Count();
            var adv = deadpool[new Random().Next(0, fCount - 1)];
            while (chooseFighter) {
                if ((currFighter != adv) && (adv.PV > 0)) {
                    return adv;
                }
                adv = deadpool[new Random().Next(fCount)];
            }
            return adv;
        }

        static private bool IsFighterAlive(Fighter fighter, List<Fighter> deadpool) {
            var life = fighter.PV;
            if (life > 0) {
                return true;
            } else {
                deadpool.Remove(fighter);
                return false;
            }
        }

        private void EndOfRound(List<Fighter> orderOfFighters, List<Fighter> deadpool) {
            foreach (var f in orderOfFighters) {
                if (f.PV <= 0) {
                    deadpool.Remove(f);
                }
            }
            if (_Turn == deadpool.Count()) {
                _Turn = 0;
                _Round++;
            }
        }

        private List<Fighter> SortFighters(List<Fighter> fighters) {
            return fighters.OrderByDescending(sp => sp.Speed).ToList();
        }
    }
}
