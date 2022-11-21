using LinearEntities.Entities.BaseEntity;

namespace LinearEntities.Entities
{
    [Serializable]
    public class LinearChannel : ILinearEntity
    {
        // Meta
        public string Id { get; set; }
        public DateTime ModifiedTime { get; set; }

        public LinearChannel(string id, string name, string shortName)
        {
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
            // Always 3 minutes for the linear project
            Duration = 180; 
            BookedSeconds = bookedSeconds;
            ChannelId = channelId;
            ChannelName = channelName;
            NextProgram = nextProgram;
            // The price is fixed and not calculated for the linear project.
            // PriceTotal should be calculated CPM price * views
            PriceTotal = 10000 * new Random().Next(1, 5); 
        }

        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
        public int BookedSeconds { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string NextProgram { get; set; }
        public double PriceTotal { get; set; }
    }
}
