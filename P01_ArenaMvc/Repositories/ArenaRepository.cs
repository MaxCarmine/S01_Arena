using P01_ArenaMvc.Models;
using P01_ArenaMvc.Models.Classes.Arena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Repositories
{

    public class ArenaRepository
    {
        public Arena coloseum { get; set; }

        private List<string> ErrorMessages;
        
        public ArenaRepository() {
            ErrorMessages = new List<string>();
        }

        public  void Start(List<Fighter> fighters) {
            coloseum = new Arena(fighters);
            coloseum.StartGame();
        }

        public  void Stop() {
             coloseum.StopGame();
        }

        //public async Task CheckStatus() {

        //}

        public List<string> GetLogs(int numberOfLogs) {
            ErrorMessages.Add("Game hasent begun nothing to show");
            try {
                if (coloseum != null) {
                    return coloseum.GetLogs(numberOfLogs);
                } 
            }catch(NullReferenceException) {
                return ErrorMessages;
                //Console.WriteLine(ex);
                //throw new NullReferenceException();
            }
            return  ErrorMessages;
        }

        
    }
}
