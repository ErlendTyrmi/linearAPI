using LinearEntities.Entities.BaseEntity;
using System.Text.Json.Serialization;

namespace LinearEntities.Entities
{
    [Serializable]
    public class AdvertiserFavorites : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        // Data
        public string[] AdvertiserIds { get; }

        public AdvertiserFavorites(string id, string[] advertiserIds)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            AdvertiserIds = advertiserIds;
        }
    }
}
