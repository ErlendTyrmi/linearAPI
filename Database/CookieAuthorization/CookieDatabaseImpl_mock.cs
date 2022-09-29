using linearAPI.Entities;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace linearAPI.Services.CookieAuthorization
{
    public class CookieDatabaseImpl_mock : CookieDatabase
    {
        private Dictionary<string, LinearUser> users = new Dictionary<string, LinearUser>();

        public LinearUser GetUser(string cookie)
        {
            var user = users.GetValueOrDefault(cookie);
            if (user == null) {
                return new LinearUser("", "", "", false);
            }
            return user;
        }

        bool CookieDatabase.SetUser(string cookie, LinearUser user)
        {
            if (users.ContainsKey(cookie))
            {
                users[cookie] = user;
            }
            else
            {
                users.Add(cookie, user);
            }

            foreach (var existingUser in users) {
               // if (existingUser)
            }

            return true;
        }
    }
}
