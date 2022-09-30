using Database.Entities;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearUser : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }

        public LinearUser(string id, string name, string email, bool isAdmin)
        {
            // Meta (inherited)
            Id = id; 
            ModifiedTime = DateTime.Now;
            CreatedTime = DateTime.Now;

            // Values
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
        }

        public string Name { get; }
        public string Email { get; }
        public bool IsAdmin { get; }
       
    }
}
