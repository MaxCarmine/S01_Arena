using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models
{
    public interface IFighter
    {
         int ID { get; set; }
         string EpicName { get; set; }
         int PV { get; set; }
         int Speed { get; set; }
         int Class { get; set; }
         int Damage { get; set; }
    }
}
