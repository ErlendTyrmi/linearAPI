using LinearEntities.Entities.BaseEntity;

namespace LinearEntities.Entities
{
    [Serializable]
    public class LinearSpotBooking : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearSpotBooking(string id, string spotId, string orderId, string agencyId)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            SpotId = spotId;
            OrderId = orderId;
            AgencyId = agencyId;
        }

        public string SpotId { get; }
        public string OrderId { get; }
        public string AgencyId { get; }
    }
}
