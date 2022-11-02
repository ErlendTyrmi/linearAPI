using linearAPI.Entities;
using linearAPI.Repo.Database;

namespace linearAPI.Repo
{
    public interface ILinearRepo
    {
        LinearAccess<LinearAdvertiser> Advertiser { get; }
        LinearAccess<LinearAgency> Agency { get; }
        LinearAccess<LinearChannel> Channel { get; }
        LinearAccess<LinearOrder> Order { get; }
        LinearAccess<LinearSalesProduct> SalesProduct { get; }
        LinearAccess<LinearSpot> Spot { get; }
        LinearAccess<LinearSpotBooking> SpotBooking { get; }
        LinearAccess<LinearUser> User { get; }
    }
}