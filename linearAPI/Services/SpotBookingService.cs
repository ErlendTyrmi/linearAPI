using Common.Interfaces;
using LinearEntities.Entities;

namespace LinearAPI.Services
{
    public class SpotBookingService : ISpotBookingService
    {
        private ILinearAccess<Order> orderRepo;
        private ILinearAccess<Advertiser> advertiserRepo;
        private ILinearAccess<SpotBooking> spotBookingRepo;
        private ILinearAccess<Spot> spotRepo;

        public SpotBookingService(ILinearAccess<Order> order, ILinearAccess<Advertiser> advertiser, ILinearAccess<SpotBooking> spotBooking, ILinearAccess<Spot> spot)
        {
            this.orderRepo = order;
            this.advertiserRepo = advertiser;
            this.spotBookingRepo = spotBooking;
            this.spotRepo = spot;
        }

        public int deleteBooking(SpotBooking booking, LinearUser user)
        {
            if (!user.CanWrite) return 403;

            var order = orderRepo.Read(booking.OrderId);
            if (order == null) return 400;

            var advertiser = advertiserRepo.Read(order.AdvertiserId);
            if (advertiser == null) return 400;

            var spot = spotRepo.Read(booking.SpotId);
            if (spot == null) return 400;

            // Admins and correct agency employees only!
            if (user.IsAdmin == false && user.AgencyId != advertiser.AgencyId) return 403;

            // Regulate order
            order.OrderTotal -= spot.PriceTotal;

            // Regulate spot
            spot.BookedSeconds -= order.DurationSeconds;

            spotRepo.Create(spot);
            orderRepo.Create(order);
            spotBookingRepo.Delete(booking.Id);

            return 200;
        }
    }
}
