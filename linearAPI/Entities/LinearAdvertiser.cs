using linearAPI.Entities.BaseEntity;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearAdvertiser : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }

        // Data
        public string Name { get; }
        public IList<String> Orders { get; set; }

        public LinearAdvertiser(string id, string name) {
            Id = id;
            Name = name;
            Orders = new List<String>();
        }

        
    }
}
