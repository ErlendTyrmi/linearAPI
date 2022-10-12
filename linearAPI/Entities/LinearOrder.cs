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

        public string Ordernumber;
        public string AdvertiserId; 
        public string AdvertiserName;
        
        public string AdvertiserProductName;

        public string HandlerId;

        public int StartWeek;
        public int EndWeek;
        public DateTime LastDate;
        
        public string ChannelId;
        public string CommercialProductId; // TV X product bought.
        public string CommercialProductName;
        
        public string OrderStatus;
       
        public double Overbudget;
        public double OrderTotal;
        public double OrderBudget;
        
        public bool IsBidAccepted;
        public bool MissingMaterial;
    }
}
