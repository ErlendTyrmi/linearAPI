using linearAPI.Entities.BaseEntity;

namespace linearAPI.Entities
{
    [Serializable]
    public class LinearChannel : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearChannel(string id, string name, string shortName) {
            Id = id;
            ModifiedTime = DateTime.Now;
            Name = name;
            ShortName = shortName;
        }

        public string Name { get; }
        public string ShortName { get; }
    }

    [Serializable]
    public class LinearSalesProduct : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearSalesProduct(string id, string name, string description)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }

    [Serializable]
    public class LinearSpot : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearSpot(string id, DateTime startDateTime, int bookedSeconds, string channelId, string channelName, string nextProgram)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            StartDateTime = startDateTime;
            Duration = 180; // Always 3 minutes :-)
            BookedSeconds = bookedSeconds;
            ChannelId = channelId;
            ChannelName = channelName;
            NextProgram = nextProgram;
            CpmPrice = $"{(50 + new Random().Next(100))} DKK"; 
        }

        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
        public int BookedSeconds { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string NextProgram { get; set; }
        public string CpmPrice { get; set; }
    }

    [Serializable]
    public class LinearSpotBooking : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearSpotBooking(string id, string spotId, string orderId)
        {
            Id = id;
            ModifiedTime = DateTime.Now;
            SpotId = spotId;
            OrderId = orderId;
        }

        public string SpotId { get; }
        public string OrderId { get; }
    }


}
