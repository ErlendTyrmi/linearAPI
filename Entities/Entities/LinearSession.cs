using LinearEntities.Entities.BaseEntity;

namespace LinearEntities.Entities
{
    public class Session : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public Session(string id, string userId)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            UserId = userId;
        }

        public string UserId { get; }
    }
}
