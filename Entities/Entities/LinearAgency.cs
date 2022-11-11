using LinearEntities.Entities.BaseEntity;

namespace LinearEntities.Entities
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

        public LinearAgency(string id, string name, string subscriptionType, bool canSeeOtherAdvertisers)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            Name = name;
            SubscriptionType = subscriptionType;
            CanSeeOtherAdvertisers = canSeeOtherAdvertisers; // TODO: Use it or lose it
        }
    }
}
