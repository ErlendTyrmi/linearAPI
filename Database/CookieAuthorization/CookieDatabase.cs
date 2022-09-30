using linearAPI.Entities;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace linearAPI.Services.CookieAuthorization
{
    public class SessionDatabase
    {
        private Dictionary<string, Session> sessions = new Dictionary<string, Session>();

        #region Singleton 

        private static SessionDatabase? testSingleton;
        
        public static SessionDatabase GetRepo() {
            testSingleton ??= new SessionDatabase();
            return testSingleton;
        }

        #endregion

        public string? GetSession(string sessionId)
        {
            Session? session = sessions.GetValueOrDefault(sessionId);

            if (session == null) return null;

            // If default value - reject
            DateTime sessionExpiration = session.Expiration;

            if (sessionExpiration.CompareTo(new DateTime()) == 0) return null;

            if (sessionExpiration.CompareTo(DateTime.Now) < 0) {
                sessions.Remove(sessionId);
                return null;
            }

            return sessionId;
        }

        public void SetSession(string userName)
        {
            sessions[userName] = new Session();
        }

        private class Session
        {
            public Session()
            {
                Expiration = DateTime.Now.AddDays(1);
            }
            public DateTime Expiration { get; }
        }
    }
}
