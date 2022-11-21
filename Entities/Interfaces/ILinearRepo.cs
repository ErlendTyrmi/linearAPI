using LinearEntities.Entities;

namespace Common.Interfaces
{
    public interface ILinearRepo
    {
        ILinearAccess<Advertiser> Advertiser { get; }
        ILinearAccess<AdvertiserFavorites> FavoriteAdvertiser { get; }
        ILinearAccess<LinearAgency> Agency { get; }
        ILinearAccess<LinearChannel> Channel { get; }
        ILinearAccess<Order> Order { get; }
        ILinearAccess<LinearSalesProduct> SalesProduct { get; }
        ILinearAccess<LinearSpot> Spot { get; }
        ILinearAccess<SpotBooking> SpotBooking { get; }
        ILinearAccess<LinearUser> User { get; }
        ILinearAccess<LinearSession> Session { get; }
    }
}