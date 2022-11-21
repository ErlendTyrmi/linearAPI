using Common.Interfaces;
using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
namespace LinearMockDatabase
{
    public class LinearRepo : ILinearRepo
    {
        private readonly ILinearAccess<Advertiser> advertiser;
        private readonly ILinearAccess<AdvertiserFavorites> favoriteAdvertiser;
        private readonly ILinearAccess<LinearAgency> agency;
        private readonly ILinearAccess<LinearChannel> channel;
        private readonly ILinearAccess<Order> order;
        private readonly ILinearAccess<LinearSalesProduct> salesProduct;
        private readonly ILinearAccess<LinearSpot> spot;
        private readonly ILinearAccess<SpotBooking> spotBooking;
        private readonly ILinearAccess<LinearUser> user;
        private readonly ILinearAccess<LinearSession> session;

        public LinearRepo(string directoryName)
        {
            advertiser = new LinearAccess<Advertiser>(directoryName);
            favoriteAdvertiser = new LinearAccess<AdvertiserFavorites>(directoryName);
            agency = new LinearAccess<LinearAgency>(directoryName);
            channel = new LinearAccess<LinearChannel>(directoryName);
            order = new LinearAccess<Order>(directoryName);
            salesProduct = new LinearAccess<LinearSalesProduct>(directoryName);
            spot = new LinearAccess<LinearSpot>(directoryName);
            spotBooking = new LinearAccess<SpotBooking>(directoryName);
            user = new LinearAccess<LinearUser>(directoryName);
            session = new LinearAccess<LinearSession>(directoryName);
        }

        // Getters

        public ILinearAccess<Advertiser> Advertiser { get => advertiser; }
        public ILinearAccess<AdvertiserFavorites> FavoriteAdvertiser {get=>favoriteAdvertiser;}
        public ILinearAccess<LinearAgency> Agency { get => agency; }
        public ILinearAccess<LinearChannel> Channel { get => channel; }
        public ILinearAccess<Order> Order { get => order; }
        public ILinearAccess<LinearSalesProduct> SalesProduct { get => salesProduct; }
        public ILinearAccess<LinearSpot> Spot { get => spot; }
        public ILinearAccess<SpotBooking> SpotBooking { get => spotBooking; }
        public ILinearAccess<LinearUser> User { get => user; }
        public ILinearAccess<LinearSession> Session { get => session; }
    }
}
