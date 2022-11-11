using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
using static LinearUtils.Util.Enums;


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
        private readonly LinearAccess<LinearChannel> channelRepo = new(dataDirectoryName);
        private readonly LinearAccess<LinearSalesProduct> productRepo = new(dataDirectoryName);
        private readonly LinearAccess<LinearSpot> spotRepo = new(dataDirectoryName);
        private readonly LinearAccess<LinearAgency> agencyRepo = new(dataDirectoryName);
        private readonly LinearAccess<LinearUser> userRepo = new(dataDirectoryName);
        private readonly LinearAccess<Advertiser> advertiserRepo = new(dataDirectoryName);
        private readonly LinearAccess<LinearOrder> orderRepo = new(dataDirectoryName);
        private readonly LinearAccess<LinearSpotBooking> spotBookingRepo = new(dataDirectoryName);

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
            IList<Advertiser> advertisers = advertiserRepo.ReadAll();

            foreach (var advertiser in advertisers)
            {
                Assert.IsNotNull(advertiser);
                Assert.IsNotNull(agencyRepo.Read(advertiser.AgencyId));
                Assert.AreEqual(agencyRepo.Read(advertiser.AgencyId)?.Id, advertiser.AgencyId);
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
        // TODO: Test agency reference
    }
}