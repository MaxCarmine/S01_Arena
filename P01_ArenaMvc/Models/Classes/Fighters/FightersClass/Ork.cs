using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models.Classes.Fighters.Fighters.Type
{
    public class Ork : Fighter
    {
        private double Strength { get; set; }

        public override void Fight(Fighter adversire) {
            var attack = CalculateDamage();
            RaiseAttack(adversire, attack);
            adversire.TakeDamage(adversire, attack);
        }

        internal override int CalculateDamage() {
            Strength = Damage;
            return (int)Strength;
        }

    }

}
