using linearAPI.Entities.BaseEntity;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearAdvertiser : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        // Data
        public string Name { get; }
        public string Agency { get; }

        public LinearAdvertiser(string id, string name, string agency) {
            Id = id;
            ModifiedTime = DateTime.Now;
            Name = name;
            Agency = agency;
        }

        
    }
}
