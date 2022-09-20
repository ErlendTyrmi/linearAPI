using linearAPI.Entities;

namespace linearAPI.Services.CookieAuthorization
{
    public interface CookieDatabase
    {
        public LinearUser getUser(string cookie);
    }
}
