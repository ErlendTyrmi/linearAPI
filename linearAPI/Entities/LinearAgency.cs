using linearAPI.Entities.BaseEntity;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearAgency : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        // Data
        public string Name { get; }
        public string SubscriptionType { get; }
        public bool CanSeeOtherAdvertisers { get; }
        // No need, this info is woth the order // public IList<string> Advertisers  { get; set; }

        public LinearAgency(string id, string name, string subscriptionType, bool canSeeOtherAdvertisers) {
            Id = id;
            ModifiedTime = DateTime.Now;
            Name = name;
            SubscriptionType = subscriptionType;
            CanSeeOtherAdvertisers = canSeeOtherAdvertisers; // TODO: Use it or lose it
            // Advertisers = new List<string>();
        }
    }
}
