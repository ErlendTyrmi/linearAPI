using LinearEntities.Entities.BaseEntity;

namespace LinearEntities.Entities
{
    [Serializable]
    public class Advertiser : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        // Data
        public string Name { get; }
        public string AgencyId { get; }

        public Advertiser(string id, string name, string agencyId)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            Name = name;
            AgencyId = agencyId;
        }


    }
}
