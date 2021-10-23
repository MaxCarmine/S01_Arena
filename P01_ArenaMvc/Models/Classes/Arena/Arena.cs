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
        public List<string> Logs { get; set; }
        private event Action<string> HasWon;
        private int _Turn { get; set; }
        private int _Round { get; set; }
        public bool GameInProgress { get; set; }

        public Arena(List<Fighter> fighters) {
            Logs = new List<string>();
            Fighters = fighters;
            AddEvent();

        }

        public async Task<List<string>> GetLogs(int numberOfLogs) {
            const string msg2 = "Arena has begun";
            Logs.Add(msg2);
            if (numberOfLogs >= 1) {
                var selectedLogs = new string[numberOfLogs];
                var lenthg = Logs.Count();
                var index = lenthg - numberOfLogs;
                Logs.CopyTo(index, selectedLogs, 0, numberOfLogs);
                return  selectedLogs.ToList();
            }
            return Logs;

        }

        private void AddEvent() {
            foreach(var f in Fighters) {
                f.Attacked += LogEvent;
                f.Damaged += LogEvent;
                f.Died += LogEvent;
            }
            HasWon += LogEvent;
        }

        private void LogEvent(string log) {
            Logs.Add(log);
            Console.WriteLine(log);
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
            while (GameInProgress) { 
                while (moreThanOneFighterIsAlive) {
                    if (GameInProgress) {
                        continue;
                    }
                    if (OneFighterLeft(deadPool)) {
                        continue;
                    }
                    StartOfRound(OrderOfAttack, deadPool);
                    await Task.Delay(1000);
                    EndOfRound(OrderOfAttack, deadPool);
                }
            }
        }

        private void Winner(List<Fighter> f) {
            var winner = f[0];
            FighterHasWon(winner);
        }
        private void FighterHasWon(Fighter winner) {
            var msg = $"{winner.EpicName} has won the games";
            HasWon?.Invoke(msg);
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
            //if (advesire.PV <= 0) {
            //    //Message.Notify($"{attacker.EpicName} has killed {advesire.EpicName}");
                
            //}
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
            //Check what fighters are left alive
            //Remove dead fighters from list Fighters.
            foreach (var f in deadpool) {
                if (f.PV <= 0) {
                    //Message.Notify($"{f.EpicName} is dead");
                }
            }
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
            var SortedList = new List<Fighter>();
            SortedList = fighters.OrderByDescending(sp => sp.Speed).ToList();
            return SortedList;

        }


    }

}
