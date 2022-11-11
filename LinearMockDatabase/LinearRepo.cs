﻿using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
namespace LinearMockDatabase
{
    public class LinearRepo : ILinearRepo
    {
        private LinearAccess<Advertiser> advertiser;
        private LinearAccess<AdvertiserFavorites> favoriteAdvertiser;
        private LinearAccess<LinearAgency> agency;
        private LinearAccess<LinearChannel> channel;
        private LinearAccess<LinearOrder> order;
        private LinearAccess<LinearSalesProduct> salesProduct;
        private LinearAccess<LinearSpot> spot;
        private LinearAccess<LinearSpotBooking> spotBooking;
        private LinearAccess<LinearUser> user;
        private LinearAccess<LinearSession> session;

        public LinearRepo(string directoryName)
        {
            advertiser = new(directoryName);
            favoriteAdvertiser = new(directoryName);
            agency = new(directoryName);
            channel = new(directoryName);
            order = new(directoryName);
            salesProduct = new(directoryName);
            spot = new(directoryName);
            spotBooking = new(directoryName);
            user = new(directoryName);
            session = new(directoryName);
        }

        // Getters

        public LinearAccess<Advertiser> Advertiser { get => advertiser; }
        public LinearAccess<AdvertiserFavorites> FavoriteAdvertiser {get=>favoriteAdvertiser;}
        public LinearAccess<LinearAgency> Agency { get => agency; }
        public LinearAccess<LinearChannel> Channel { get => channel; }
        public LinearAccess<LinearOrder> Order { get => order; }
        public LinearAccess<LinearSalesProduct> SalesProduct { get => salesProduct; }
        public LinearAccess<LinearSpot> Spot { get => spot; }
        public LinearAccess<LinearSpotBooking> SpotBooking { get => spotBooking; }
        public LinearAccess<LinearUser> User { get => user; }
        public LinearAccess<LinearSession> Session { get => session; }
    }
}