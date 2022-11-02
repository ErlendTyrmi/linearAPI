using linearAPI.Entities;
using linearAPI.Repo;
using linearAPI.Repo.Database;
using System.Collections;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace linearAPI.Services
{
    public class SessionService : ISessionService
    {
        IList<LinearUser> users = new LinearAccess<LinearUser>("Generated/").ReadAll();

        public LinearUser? getUser(string username)
        {

            foreach (LinearUser user in users)
            {
                if (user.UserName == username)
                {
                    return user;
                }
            }
            return default;
        }
    }
}
