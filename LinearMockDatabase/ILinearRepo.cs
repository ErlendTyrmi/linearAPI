using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;

namespace LinearMockDatabase
{
    public interface ILinearRepo
    {
        LinearAccess<Advertiser> Advertiser { get; }
        LinearAccess<AdvertiserFavorites> FavoriteAdvertiser {get;}
        LinearAccess<LinearAgency> Agency { get; }
        LinearAccess<LinearChannel> Channel { get; }
        LinearAccess<LinearOrder> Order { get; }
        LinearAccess<LinearSalesProduct> SalesProduct { get; }
        LinearAccess<LinearSpot> Spot { get; }
        LinearAccess<LinearSpotBooking> SpotBooking { get; }
        LinearAccess<LinearUser> User { get; }
        LinearAccess<LinearSession> Session { get; }
    }
}