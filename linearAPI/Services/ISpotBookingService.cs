using LinearEntities.Entities;

namespace LinearAPI.Services
{
    public interface ISpotBookingService
    {
        SpotBooking? Get(string id);
        IList<SpotBooking>? GetByUser(LinearUser user);
        int deleteBooking(SpotBooking booking, LinearUser user);
    }
}