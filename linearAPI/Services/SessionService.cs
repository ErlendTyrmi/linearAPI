using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;

namespace LinearAPI.Services
{
    public class SessionService : ISessionService
    {
        private LinearAccess<LinearUser> userRepo;
        private LinearAccess<LinearSession> sessionRepo;
        private TimeSpan timeout;

        public SessionService(LinearAccess<LinearUser> userRepo, LinearAccess<LinearSession> sessionRepo, TimeSpan timeout)
        {
            this.userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            this.sessionRepo = sessionRepo ?? throw new ArgumentNullException(nameof(sessionRepo));
            this.timeout = timeout;
        }

        public LinearUser? getUserFromUserName(string username)
        {
            var users = userRepo.ReadAll();
            foreach (LinearUser user in users)
            {
                if (user.UserName == username)
                {
                    return user;
                }
            }
            return default;
        }

        public string SignIn(LinearUser user)
        {
            var id = Guid.NewGuid().ToString();
            sessionRepo.Create(new LinearSession(id, user.Id));
            return id;
        }

        public LinearUser? AssertSignedIn(string? sessionId)
        {
            if (sessionId == null) return null;

            var session = sessionRepo.Read(sessionId);
            if (session == null) return null;

            if (IsExpired(session))
            {
                RemoveExpiredSessions();
                return null;
            }

            var user = userRepo.Read(session.UserId);
            return user;
        }

        public void SignOut(string sessionId)
        {
            sessionRepo.Delete(sessionId);
        }

        private bool IsExpired(LinearSession session)
        {
            DateTime expiration = DateTime.Now - timeout;
            return session.ModifiedTime.CompareTo(expiration) < 0;
        }

        private void RemoveExpiredSessions()
        {
            var sessions = sessionRepo.ReadAll();

            List<string> expiredSessionIds = new();

            foreach (var session in sessions)
            {
                if (IsExpired(session))
                {
                    expiredSessionIds.Add(session.Id);
                }
            }
            sessionRepo.DeleteList(expiredSessionIds);
        }

    }
}
