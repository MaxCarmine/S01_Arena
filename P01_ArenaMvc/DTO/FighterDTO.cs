using P01_ArenaMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.DTO
{
    public class FighterDTO 
    {
        public string EpicName { get ; set ; }
        public int PV { get; set ; }
        public int Speed { get ; set ; }
        public int Class { get ; set; }
        public int Damage { get; set; }
    }
}
