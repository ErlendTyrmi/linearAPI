using linearAPI.Entities;

namespace linearAPI.Services.CookieAuthorization
{
    public interface CookieDatabase
    {
        public LinearUser getUser(string cookie);
        public bool setUser(string cookie, LinearUser user);
    }
}
