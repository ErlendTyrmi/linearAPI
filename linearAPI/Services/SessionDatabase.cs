using linearAPI.Entities;
using linearAPI.Repo;
using System.Collections;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace linearAPI.Services
{
    public class SessionService
    {
        #region Singleton 

        private static SessionService? testSingleton;

        public static SessionService GetRepo()
        {
            testSingleton ??= new SessionService();
            return testSingleton;
        }

        #endregion

        public LinearUser? getUser(string username)
        {

            IList<LinearUser> users = new LinearRepo<LinearUser>().ReadAll();

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
