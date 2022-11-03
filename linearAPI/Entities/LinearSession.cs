using linearAPI.Entities.BaseEntity;

namespace linearAPI.Entities
{
    public class LinearSession: ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearSession(string id, String userId)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            UserId = userId;
        }

        public string UserId { get; }
    }
}
