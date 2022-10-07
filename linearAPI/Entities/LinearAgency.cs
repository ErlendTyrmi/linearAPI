using linearAPI.Entities.BaseEntity;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearAgency : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }

        // Data
        public string Name { get; }
        public string SubscriptionType { get; }
        public bool CanSeeOtherAdvertisers { get; }
        public IList<string> Advertisers  { get; set; }

        public LinearAgency(string id, string name, string subscriptionType, bool canSeeOtherAdvertisers) {
        Id = id;
            Name = name;
            SubscriptionType = subscriptionType;
            CanSeeOtherAdvertisers = canSeeOtherAdvertisers; // TODO: User it or lose it
            Advertisers = new List<string>();
        }
    }
}
