using linearAPI.Entities;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace linearAPI.Services.CookieAuthorization
{
    public class CookieDatabaseImpl_mock : CookieDatabase
    {
        private Dictionary<Cookie, LinearUser> users = new Dictionary<Cookie, LinearUser>();

        public LinearUser GetUser(Cookie cookie)
        {
            var user = users.GetValueOrDefault(cookie);
            if (user == null) {
                return new LinearUser("");
            }
            return user;
        }

        bool CookieDatabase.SetUser(Cookie cookie, LinearUser user)
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
