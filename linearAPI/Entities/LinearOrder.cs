using linearAPI.Entities.BaseEntity;
using linearAPI.Util;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearOrder : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public string Ordernumber { get; set; }
        public string AdvertiserId { get; set; }
        public string AdvertiserName { get; set; }

        public LinearOrder(string id, DateTime modifiedTime, string ordernumber, string advertiserId, string advertiserName, 
            string advertiserProductName, string handlerId, int startWeek, int endWeek, string orderTypeName, string channelId, 
            string salesProductId, string salesProductName, string? salesGroupNumber,int durationSeconds, double costPerMille, int viewsExpectedMille, 
            int viewsDeliveredMille, string orderStatus, double orderBudget, double orderTotal)
        {
            Id = id;
            ModifiedTime = modifiedTime;
            Ordernumber = ordernumber;
            AdvertiserId = advertiserId;
            AdvertiserName = advertiserName;
            AdvertiserProductName = advertiserProductName;
            HandlerId = handlerId;
            StartWeek = startWeek;
            EndWeek = endWeek;
            OrderTypeName = orderTypeName;
            ChannelId = channelId;
            SalesProductId = salesProductId;
            SalesProductName = salesProductName;
            SalesGroupNumber = salesGroupNumber;
            DurationSeconds = durationSeconds;
            CostPerMille = costPerMille;
            ViewsExpectedMille = viewsExpectedMille;
            ViewsDeliveredMille = viewsDeliveredMille;
            OrderStatus = orderStatus;
            OrderBudget = orderBudget;
            OrderTotal = orderTotal;
        }

        public string AdvertiserProductName { get; set; } // The product being advertised in the commercial

        public string HandlerId { get; set; }

        public int StartWeek { get; set; }
        public int EndWeek { get; set; }

        public string OrderTypeName { get; set; } // Exposure or Specific - ENUM

        public string ChannelId { get; set; }

        public string SalesProductId { get; set; } // The TV Commercial that is bought
        public string SalesProductName { get; set; }
        // A number linking order to other grouper orders, if salesproduct has this property
        public string? SalesGroupNumber { get; set; }

        public int DurationSeconds { get; set; }

        public double CostPerMille { get; set; }
        public int ViewsExpectedMille { get; set; }
        public int ViewsDeliveredMille { get; set; }

        public string OrderStatus { get; set; }

        public double OrderBudget { get; set; }
        public double OrderTotal { get; set; }

        [JsonIgnore]
        public bool OrderBudgetExceeded {
            get { return OrderTotal > OrderBudget; }
        }

    }
}
