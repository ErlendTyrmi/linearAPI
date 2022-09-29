using linearAPI.Entities;
using System.Net;

namespace linearAPI.Services.CookieAuthorization
{
    public interface CookieDatabase
    {
        public LinearUser GetUser(Cookie cookie);
        public bool SetUser(Cookie cookie, LinearUser user);
    }
}
