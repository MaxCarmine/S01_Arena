using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models.Classes.Fighters.Fighters.Type
{
    public class Wizard : Fighter
    {
        private double _MaximumPower { get; set; }

        private double _Magic;

        public override void Fight(Fighter adversire) {
            var attack = CalculateDamage();
            RaiseAttack(adversire, attack);
            adversire.TakeDamage(adversire,attack);
        }

        internal override int CalculateDamage() {
            var rand = new Random();
            _Magic = rand.NextDouble();
            _MaximumPower = Damage;
            var attack = _Magic * _MaximumPower;
            return (int)attack;
        }
        
    }
}
