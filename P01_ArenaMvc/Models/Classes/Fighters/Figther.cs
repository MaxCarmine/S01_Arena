using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models
{
    abstract public class Fighter : IFighter
    {
        public event Action<string> Died;
        public event Action<string> Damaged;
        public event Action<string> Attacked;

        public int ID { get; set; }  
        public string EpicName { get; set; }
        public int PV { get; set; }
        public int Speed { get; set; }
        public int Class { get; set; }
        public int Damage { get; set; }

        public virtual void Fight(Fighter adversire) {
            var attack = CalculateDamage();
            OnAttack(adversire.EpicName,attack);
            adversire.TakeDamage(attack);
        }

        internal abstract int CalculateDamage();

        internal void TakeDamage(int damage) {
            PV -= damage;
            OnHit(damage);
            if (PV < 1) {
                OnDeath();
            }
            
        }

        internal void OnHit(int damage) {
            var msg = $"{EpicName} has been DAMAGED with {damage} !!!";
            Damaged?.Invoke(msg);
        }

        internal void OnAttack(string advName, int damage) {
            var msg = $"{EpicName} has attacked {advName} ===> dealing {damage} damage";
            Attacked?.Invoke(msg);
        }

        internal void OnDeath() {
            var msg = $"{EpicName} has DIED!!!";
            Died?.Invoke(msg);
        }
    }
}
