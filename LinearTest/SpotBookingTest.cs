using LinearAPI.Services;
using LinearEntities.Entities;
using LinearMockDatabase.Database;
using LinearMockDatabase.Repo.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LinearUtils.Util.Enums;


namespace LinearTest
{
    /// <summary>
    /// Test references ("Foreign keys") in dataset
    /// </summary>
    [TestClass]
    public class SpotBookingTest
    {
        // Must match generated data folder name
        private static string dataDirectoryName = "TestData/";

        // Repos
        private readonly LinearAccess<Channel> channelRepo = new(dataDirectoryName);
        private readonly LinearAccess<SalesProduct> productRepo = new(dataDirectoryName);
        private readonly LinearAccess<Spot> spotRepo = new(dataDirectoryName);
        private readonly LinearAccess<Agency> agencyRepo = new(dataDirectoryName);
        private readonly LinearAccess<LinearUser> userRepo = new(dataDirectoryName);
        private readonly LinearAccess<Advertiser> advertiserRepo = new(dataDirectoryName);
        private readonly LinearAccess<Order> orderRepo = new(dataDirectoryName);
        private readonly LinearAccess<SpotBooking> spotBookingRepo = new(dataDirectoryName);

        private ISpotBookingService? spotBookingService;
        private LinearUser? adminUser;
        private LinearUser? bureauUser;

        [TestInitialize]
        public void TestInitialize()
        {
            DeleteTestData();
            DataGenerator.Generate(dataDirectoryName);

            spotBookingService = new SpotBookingService(orderRepo, advertiserRepo, spotBookingRepo, spotRepo);
            adminUser = new LinearUser("uniqueId123", "Test Admin", "tead", "testad@tyrmi.com", "TestAgencyId", true, true, true, true); // Last true means admin
            bureauUser = new LinearUser("uniqueId456", "Test Bureeau", "tebu", "testbu@tyrmi.com", agencyRepo.ReadAll().First((it)=>{return it.Name.ToLower().Contains("tvx");}).Id, true, true, true, false);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTestData();
        }

        [TestMethod]
        public void TestSpotBookingDelete()
        {
            for (int i = 0; i < 30; i++)
            {
                var bookings = spotBookingRepo.ReadAll();
                Assert.IsNotNull(bookings.ElementAt(i));
                var booking = bookings.ElementAt(i);
                Assert.IsNotNull(booking.OrderId);

                var order = orderRepo.Read(booking.OrderId);
                Assert.IsNotNull(order);
                Assert.IsNotNull(order.OrderTotal);
                Assert.IsFalse(order.OrderTotal < 1);

                var spot = spotRepo.Read(booking.SpotId);
                Assert.IsNotNull(spot);
                Assert.IsNotNull(spot.BookedSeconds);
                Assert.IsNotNull(spot.PriceTotal);

                spotBookingService.deleteBooking(booking, adminUser);

                var newBookings = spotBookingRepo.ReadAll();
                Assert.IsNotNull(bookings.ElementAt(0));
                var exists = newBookings.Any((it) => it.Id == booking.Id);
                Assert.IsFalse(exists);

                var newOrder = orderRepo.Read(booking.OrderId);
                Assert.IsNotNull(newOrder);
                Assert.IsNotNull(newOrder.OrderTotal);
                var budgetDiff = (order.OrderTotal - newOrder.OrderTotal) - spot.PriceTotal;
                Assert.IsTrue(Math.Abs(budgetDiff) <= 0.01);

                var newSpot = spotRepo.Read(booking.SpotId);
                Assert.IsNotNull(newSpot);
                Assert.IsNotNull(newSpot.BookedSeconds);
                var timeDiff = (spot.BookedSeconds - newSpot.BookedSeconds) - order.DurationSeconds;
                Assert.AreEqual(Math.Abs(timeDiff), 0);
            }
        }

        [TestMethod]
        public void TestSpotBookingDeleteNonAdmin()
        {
            
                var bookings = spotBookingRepo.ReadAll();
                var booking = bookings.First((it)=>{return it.AgencyId == bureauUser.AgencyId;});
                Assert.IsNotNull(booking.OrderId);

                var order = orderRepo.Read(booking.OrderId);
                Assert.IsNotNull(order);
                Assert.IsNotNull(order.OrderTotal);
                Assert.IsFalse(order.OrderTotal < 1);

                var spot = spotRepo.Read(booking.SpotId);
                Assert.IsNotNull(spot);
                Assert.IsNotNull(spot.BookedSeconds);
                Assert.IsNotNull(spot.PriceTotal);

                spotBookingService.deleteBooking(booking, bureauUser);

                var newBookings = spotBookingRepo.ReadAll();
                Assert.IsNotNull(bookings.ElementAt(0));
                var exists = newBookings.Any((it) => it.Id == booking.Id);
                Assert.IsFalse(exists);

                var newOrder = orderRepo.Read(booking.OrderId);
                Assert.IsNotNull(newOrder);
                Assert.IsNotNull(newOrder.OrderTotal);
                var budgetDiff = (order.OrderTotal - newOrder.OrderTotal) - spot.PriceTotal;
                Assert.IsTrue(Math.Abs(budgetDiff) <= 0.01);

                var newSpot = spotRepo.Read(booking.SpotId);
                Assert.IsNotNull(newSpot);
                Assert.IsNotNull(newSpot.BookedSeconds);
                var timeDiff = (spot.BookedSeconds - newSpot.BookedSeconds) - order.DurationSeconds;
                Assert.AreEqual(Math.Abs(timeDiff), 0);
        }

        private void DeleteTestData()
        {
            channelRepo.DeleteAll();
            productRepo.DeleteAll();
            spotRepo.DeleteAll();
            agencyRepo.DeleteAll();
            userRepo.DeleteAll();
            advertiserRepo.DeleteAll();
            orderRepo.DeleteAll();
            spotBookingRepo.DeleteAll();
        }
    }
}