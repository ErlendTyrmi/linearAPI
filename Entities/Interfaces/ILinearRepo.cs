using LinearEntities.Entities;

namespace Common.Interfaces
{
    public interface ILinearRepo
    {
        ILinearAccess<Advertiser> Advertiser { get; }
        ILinearAccess<AdvertiserFavorites> FavoriteAdvertiser { get; }
        ILinearAccess<Agency> Agency { get; }
        ILinearAccess<Channel> Channel { get; }
        ILinearAccess<Order> Order { get; }
        ILinearAccess<SalesProduct> SalesProduct { get; }
        ILinearAccess<Spot> Spot { get; }
        ILinearAccess<SpotBooking> SpotBooking { get; }
        ILinearAccess<LinearUser> User { get; }
        ILinearAccess<Session> Session { get; }
    }
}