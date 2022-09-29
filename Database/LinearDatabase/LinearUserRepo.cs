using linearAPI.Entities;
using System.Text.Json;

namespace Database.LinearDatabase
{
    public class LinearUserRepo
    {
        LinearFileHandler fileHandler = new LinearFileHandler(null);
        readonly string EntityName = nameof(LinearUser);

        public void create(LinearUser user)
        {
            var users = ReadAllAsDictionary();
            if (users == null) users = new Dictionary<string, LinearUser>();

            if (users.ContainsKey(user.Id))
            {
                users[user.Id] = user;
            }
            else
            {
                users.Add(user.Id, user);
            }

            var usersString = JsonSerializer.Serialize(users);

            fileHandler.WriteAsString(EntityName, usersString);
        }

        // Optional
        public LinearUser? Read(string id)
        {
            var users = ReadAllAsDictionary();

            if (users == null) return null;

            foreach (var userKey in users)
            {
                if (userKey.Value.Id == id) return userKey.Value;
            }

            return null;
        }

        public IList<LinearUser> ReadAll()
        {
            var userDict = ReadAllAsDictionary();
            var userList = new List<LinearUser>();

            if (userDict == null || userDict.Count == 0) return userList;

            foreach (var userEntry in userDict) {
                userList.Add(userEntry.Value);
            }
            return userList;
        }

        // PRIVATE METHODS

        private IDictionary<string, LinearUser>? ReadAllAsDictionary()
        {
            string? userString = fileHandler.ReadAsString(EntityName);

            if (userString == null) return null;

            Dictionary<string, LinearUser>? users = JsonSerializer.Deserialize<Dictionary<string, LinearUser>>(userString);
            return users;
        }
    }
}
