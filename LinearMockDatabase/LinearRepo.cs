using Common.Interfaces;
using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
namespace LinearMockDatabase
{
    public class LinearRepo : ILinearRepo
    {
        private readonly ILinearAccess<Advertiser> advertiser;
        private readonly ILinearAccess<AdvertiserFavorites> favoriteAdvertiser;
        private readonly ILinearAccess<Agency> agency;
        private readonly ILinearAccess<Channel> channel;
        private readonly ILinearAccess<Order> order;
        private readonly ILinearAccess<SalesProduct> salesProduct;
        private readonly ILinearAccess<Spot> spot;
        private readonly ILinearAccess<SpotBooking> spotBooking;
        private readonly ILinearAccess<LinearUser> user;
        private readonly ILinearAccess<Session> session;

        public LinearRepo(string directoryName)
        {
            advertiser = new LinearAccess<Advertiser>(directoryName);
            favoriteAdvertiser = new LinearAccess<AdvertiserFavorites>(directoryName);
            agency = new LinearAccess<Agency>(directoryName);
            channel = new LinearAccess<Channel>(directoryName);
            order = new LinearAccess<Order>(directoryName);
            salesProduct = new LinearAccess<SalesProduct>(directoryName);
            spot = new LinearAccess<Spot>(directoryName);
            spotBooking = new LinearAccess<SpotBooking>(directoryName);
            user = new LinearAccess<LinearUser>(directoryName);
            session = new LinearAccess<Session>(directoryName);
        }

        // Getters

        public ILinearAccess<Advertiser> Advertiser { get => advertiser; }
        public ILinearAccess<AdvertiserFavorites> FavoriteAdvertiser {get=>favoriteAdvertiser;}
        public ILinearAccess<Agency> Agency { get => agency; }
        public ILinearAccess<Channel> Channel { get => channel; }
        public ILinearAccess<Order> Order { get => order; }
        public ILinearAccess<SalesProduct> SalesProduct { get => salesProduct; }
        public ILinearAccess<Spot> Spot { get => spot; }
        public ILinearAccess<SpotBooking> SpotBooking { get => spotBooking; }
        public ILinearAccess<LinearUser> User { get => user; }
        public ILinearAccess<Session> Session { get => session; }
    }
}
