using LinearEntities.Entities;

namespace LinearAPI.Services
{
    public interface ISpotBookingService
    {
        int deleteBooking(SpotBooking booking, LinearUser user);
    }
}