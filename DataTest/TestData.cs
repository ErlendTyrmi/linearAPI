using linearAPI.Entities;
using linearAPI.Repo;
using static linearAPI.Util.Enums;

namespace LinearTest
{
    /// <summary>
    /// Test references ("Foreign keys") in dataset
    /// </summary>
    [TestClass]
    public class DataTest
    {
        // Must match generated data folder name
        private static string dataDirectoryName = "Generated/";

        // Repos
        private readonly LinearRepo<LinearChannel> channelRepo = new(dataDirectoryName);
        private readonly LinearRepo<LinearSalesProduct> productRepo = new(dataDirectoryName);
        private readonly LinearRepo<LinearSpot> spotRepo = new(dataDirectoryName);
        private readonly LinearRepo<LinearAgency> agencyRepo = new(dataDirectoryName);
        private readonly LinearRepo<LinearUser> userRepo = new(dataDirectoryName);
        private readonly LinearRepo<LinearAdvertiser> advertiserRepo = new(dataDirectoryName);
        private readonly LinearRepo<LinearOrder> orderRepo = new(dataDirectoryName);
        private readonly LinearRepo<LinearSpotBooking> spotBookingRepo = new(dataDirectoryName);

        [TestMethod]
        public void TestSpot()
        {
            // Spot           
            var spotList = spotRepo.ReadAll();

            foreach (var spot in spotList)
            {
                //Console.WriteLine(spot.Id);
                Assert.IsNotNull(spot);
                Assert.IsNotNull(channelRepo.Read(spot.ChannelId));
                Assert.AreEqual(channelRepo.Read(spot.ChannelId)?.Id, spot.ChannelId);
                Assert.AreEqual(channelRepo.Read(spot.ChannelId)?.Name, spot.ChannelName);
            }
        }

        [TestMethod]
        public void TestUsers()
        {
            var userList = userRepo.ReadAll();

            foreach (var user in userList)
            {
                Assert.IsNotNull(user);
                Assert.IsNotNull(agencyRepo.Read(user.AgencyId));
                Assert.AreEqual(agencyRepo.Read(user.AgencyId)?.Id, user.AgencyId);
            }
        }

        [TestMethod]
        public void TestAdvertisers()
        {
            IList<LinearAdvertiser> advertisers = advertiserRepo.ReadAll();

            foreach (var advertiser in advertisers)
            {
                Assert.IsNotNull(advertiser);
                Assert.IsNotNull(agencyRepo.Read(advertiser.Agency));
                Assert.AreEqual(agencyRepo.Read(advertiser.Agency)?.Id, advertiser.Agency);
            }
        }

        [TestMethod]
        public void TestOrders()
        {

            IList<LinearOrder> orders = orderRepo.ReadAll();

            foreach (var order in orders)
            {
                Assert.IsNotNull(order);

                // Handler
                Assert.IsNotNull(userRepo.Read(order.HandlerId));
                Assert.AreEqual(userRepo.Read(order.HandlerId)?.Id, order.HandlerId);

                // Advertiser
                Console.WriteLine(order.AdvertiserId);
                Assert.IsNotNull(advertiserRepo.Read(order.AdvertiserId));
                Assert.AreEqual(advertiserRepo.Read(order.AdvertiserId)?.Id, order.AdvertiserId);
                Assert.AreEqual(advertiserRepo.Read(order.AdvertiserId)?.Name, order.AdvertiserName);

                // Channel
                Assert.IsNotNull(channelRepo.Read(order.ChannelId));
            }
        }
        
        [TestMethod]
        public void TestSpotBooking()
        {
            // SpotBooking
            var bookings = spotBookingRepo.ReadAll();

            foreach (var booking in bookings) {
                // Check spot exists
                Assert.IsNotNull(spotRepo.Read(booking.SpotId));

                // Check spot channel exists
                var channelId = spotRepo.Read(booking.SpotId)?.ChannelId;
                Assert.IsNotNull(channelId);
                Assert.IsNotNull(channelRepo.Read(channelId));

                // Check order exists and is "specific"
                var order = orderRepo.Read(booking.OrderId);
                Assert.IsNotNull(order);
                Assert.AreEqual(order.OrderTypeName, OrderTypeName.specific.ToString());
            }
        }
    }
}