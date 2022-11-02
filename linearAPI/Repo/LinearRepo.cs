using linearAPI.Entities;
using linearAPI.Repo.Database;

namespace linearAPI.Repo
{
    public class LinearRepo : ILinearRepo
    {
        static string directoryName = "Generated/";

        private LinearAccess<LinearAdvertiser> advertiser = new(directoryName);
        private LinearAccess<LinearAgency> agency = new(directoryName);
        private LinearAccess<LinearChannel> channel = new(directoryName);
        private LinearAccess<LinearOrder> order = new(directoryName);
        private LinearAccess<LinearSalesProduct> salesProduct = new(directoryName);
        private LinearAccess<LinearSpot> spot = new(directoryName);
        private LinearAccess<LinearSpotBooking> spotBooking = new(directoryName);
        private LinearAccess<LinearUser> user = new(directoryName);

        public LinearAccess<LinearAdvertiser> Advertiser { get => advertiser; }
        public LinearAccess<LinearAgency> Agency { get => agency; }
        public LinearAccess<LinearChannel> Channel { get => channel; }
        public LinearAccess<LinearOrder> Order { get => order; }
        public LinearAccess<LinearSalesProduct> SalesProduct { get => salesProduct; }
        public LinearAccess<LinearSpot> Spot { get => spot; }
        public LinearAccess<LinearSpotBooking> SpotBooking { get => spotBooking; }
        public LinearAccess<LinearUser> User { get => user; }
    }
}
