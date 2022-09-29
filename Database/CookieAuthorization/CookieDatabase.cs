using linearAPI.Entities;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace linearAPI.Services.CookieAuthorization
{
    public class CookieDatabase
    {
        private Dictionary<string, DateTime> sessions = new Dictionary<string, DateTime>();

        public bool sessionActive(string id)
        {
            DateTime sessionExpiration = sessions.GetValueOrDefault(id);

            // If default value - reject
            if (sessionExpiration.CompareTo(new DateTime()) == 0) return false;

            if (sessionExpiration.CompareTo(DateTime.Now) > 0) {
                sessions.Remove(id);
                return false;
            }

            return true;
        }

        public void SetSession(string id)
        {
            sessions[id] = DateTime.Now.AddHours(18);
        }
    }
}
