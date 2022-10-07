using linearAPI.Entities.BaseEntity;
using System.Threading.Channels;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearOrder : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }

        public string Ordernumber;
        public string AdvertiserId; // This is the link to advertiser? Please reverse!

        public int StartWeek;
        public int EndWeek;
        public DateTime LastDate;
        
        public string ChannelId;
        public string CommercialProductId; // Name of actual product Use simple name instead of table?
        
        public string OrderStatusId;
       
        public bool Overbudget;
        public double OrderTotal;
        public double OrderBudget;
        
        public bool IsBidAccepted;
        public bool MissingMaterial;
    }
}
