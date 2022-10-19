using linearAPI.Entities;
using System.Net;

namespace linearAPI.Services.CookieAuthorization
{
    public interface CookieDatabase
    {
        public LinearUser GetUser(string cookie);
        public bool SetUser(string cookie, LinearUser user);
    }
}
