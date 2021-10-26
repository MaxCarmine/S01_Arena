using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models.Classes
{
    public class OutputManager
    {
        public List<string> Logs { get; set; }

        public OutputManager() {
            Logs = new List<string>();
        }
        public void Attack(Fighter fighter, int damage) {
            var msgAttack = $"{fighter.EpicName} attacked, dealing {damage} damage";
            LogEvent(msgAttack);
            Console.WriteLine(msgAttack);
        }

        public void Hit(Fighter fighter, int damage) {
            var msgHit = $"{fighter.EpicName} has been attacked, receving {damage} damage";
            LogEvent(msgHit);
            Console.WriteLine(msgHit);
        }

        public void Died(Fighter fighter) {
            var msgDied = $"{fighter.EpicName} has died";
            LogEvent(msgDied);
            Console.WriteLine(msgDied);
        }

        public void Won(Fighter winner) {
            var msgWon = $"{winner.EpicName} has won the game";
            LogEvent(msgWon);
            Console.WriteLine(msgWon);
        }

        private void LogEvent(string fevent) {
            Logs.Add(fevent);
        }
    }
}
