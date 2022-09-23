using linearAPI.Entities;
using System.Reflection.Metadata.Ecma335;

namespace linearAPI.Services.CookieAuthorization
{
    public class CookieDatabaseImpl_mock : CookieDatabase
    {
        private Dictionary<string, LinearUser> users = new Dictionary<string, LinearUser>();

        public LinearUser getUser(string cookie)
        {
            var user = users.GetValueOrDefault(cookie);
            if (user == null) {
                return new LinearUser("");
            }
            return user;
        }

        public void setUser(string cookie, LinearUser user)
        {
            if (users.ContainsKey(cookie))
            {
                users[cookie] = user;
            }
            else
            {
                users.Add(cookie, user);
            }
        }

        bool CookieDatabase.setUser(string cookie, LinearUser user)
        {
            throw new NotImplementedException();
        }
    }
}
