using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models.Classes.Fighters.Fighters.Type
{
    public class Warrior : Fighter
    {
        private double DamagePercentage { get; set; }

        public override void Fight(Fighter adversire) {
            var attack = (CalculateDamage() * adversire.PV) / 100;
            OnAttack(adversire.EpicName, attack);
            adversire.TakeDamage(attack);
        }


        internal override int CalculateDamage() {
            return (int)DamagePercentage;
        }
    }
}
