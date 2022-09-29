namespace linearAPI.Entities
{
    [Serializable]
    public class LinearUser
    {
        public LinearUser(string id, string name, string email, bool admin)
        {
            Id = id;
            Name = name;
            Email = email;
            Admin = admin;
        }

        public string Id { get; }
        public string Name { get; }
        public string Email { get; }
        public bool Admin { get; }
    }
}
