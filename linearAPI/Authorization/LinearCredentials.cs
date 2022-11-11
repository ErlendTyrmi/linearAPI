using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Authorization
{
    public class LinearCredentials
    {
        public LinearCredentials(string password, string username){
            this.Password = password;
            this.Username = username;
        }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
