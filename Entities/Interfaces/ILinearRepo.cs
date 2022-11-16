using LinearEntities.Entities;

namespace Common.Interfaces
{
    public interface ILinearRepo
    {
        ILinearAccess<Advertiser> Advertiser { get; }
        ILinearAccess<AdvertiserFavorites> FavoriteAdvertiser { get; }
        ILinearAccess<LinearAgency> Agency { get; }
        ILinearAccess<LinearChannel> Channel { get; }
        ILinearAccess<LinearOrder> Order { get; }
        ILinearAccess<LinearSalesProduct> SalesProduct { get; }
        ILinearAccess<LinearSpot> Spot { get; }
        ILinearAccess<LinearSpotBooking> SpotBooking { get; }
        ILinearAccess<LinearUser> User { get; }
        ILinearAccess<LinearSession> Session { get; }
    }
}