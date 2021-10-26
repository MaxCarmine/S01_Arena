using P01_ArenaMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Entity
{
    
    public class FighterEntity 
    {
        public int Id { get; set; }
        public string EpicName { get; set; }
        public int PV { get; set; }
        public int Speed { get; set; }
        public double Damage { get; set; }
        public int Class { get; set; }
    }
}
