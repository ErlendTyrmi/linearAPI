using linearAPI.Entities.BaseEntity;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearUser : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }

        public LinearUser(
            string id, 
            string name, string username, string email, string agencyId, 
            bool canDownloadMaterial = false, bool canWrite = false, bool canRead = true)
        {
            // Meta (inherited)
            Id = id; 
            ModifiedTime = DateTime.Now;
            CreatedTime = DateTime.Now;

            // Values
            Name = name;
            UserName = username;
            Email = email;
            AgencyId = agencyId;

            // Permissions
            CanDownloadMaterial = canDownloadMaterial;
            CanWrite = canWrite;
            CanRead = canRead;
        }

        public string Name { get; }
        public string Email { get; }
        public string AgencyId { get; }
        public string UserName { get; }
        public bool CanDownloadMaterial { get; }
        public bool CanWrite { get; } 
        public bool CanRead { get; }
    }
}
