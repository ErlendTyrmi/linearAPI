using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Authorization
{
    public static class LinearAuthentication
    {
        public static bool AuthenticateCredentials(LinearCredentials credentials)
        {

            if (credentials == null) return false;

            if (credentials.Username == null) return false;

            if (credentials.Username.Length < 1) return false;

            if (credentials.Username == credentials.Password) return true;

            return false;
        }
    }
}
