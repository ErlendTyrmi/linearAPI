using LinearEntities.Entities.BaseEntity;

namespace LinearEntities.Entities
{
    public class LinearSession : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearSession(string id, string userId)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            UserId = userId;
        }

        public string UserId { get; }
    }
}
