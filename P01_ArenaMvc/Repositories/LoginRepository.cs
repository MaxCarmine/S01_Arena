using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Repositories
{
    public class LoginRepository
    {
        private const string Admin = "Admin";
        public bool UserExists(string username) {
            if (username.Equals(Admin)) {
                return true;
            }
            return false;
        }
    }
}
