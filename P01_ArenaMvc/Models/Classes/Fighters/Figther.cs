using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models
{
    abstract public class Fighter : IFighter
    {
        public event Action<Fighter> Died;
        public event Action<Fighter,int> Damaged;
        public event Action<Fighter,int> Attacked;

        public int ID { get; set; }  
        public string EpicName { get; set; }
        public int PV { get; set; }
        public int Speed { get; set; }
        public int Class { get; set; }
        public int Damage { get; set; }

        public virtual void Fight(Fighter adversire) {
            var attack = CalculateDamage();
            RaiseAttack(adversire,attack);
            adversire.TakeDamage(adversire,attack);
        }

        internal abstract int CalculateDamage();

        internal void TakeDamage(Fighter fighter, int damage) {
            PV -= damage;
            RaiseHit(fighter,damage);
            if (PV < 1) {
                RaiseDeath(fighter);
            }
            
        }

        internal void RaiseHit(Fighter figher, int damage) {
            Damaged?.Invoke(figher,damage);
        }

        internal void RaiseAttack(Fighter figher, int damage) {
            Attacked?.Invoke(figher, damage);
        }

        internal void RaiseDeath(Fighter fighter) {
            Died?.Invoke(fighter);
        }
    }
}
