using linearAPI.Entities;
using linearAPI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using static linearAPI.Util.Enums;

namespace linearAPI.Repo.Database
{
    /// <summary>
    /// Making Mock Data. Overwrites existing data.
    /// </summary>
    public class DataGenerator
    {
        private static string dataDirectoryName = "Generated/";
        private static readonly List<string> CampaignWords = GetCampaignWords();

        public static void Generate()
        {
            // This data does not change
            #region StaticData

            // Channels
            var channelRepo = new LinearDatabase<LinearChannel>(dataDirectoryName);
            channelRepo.DeleteAll();
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX National", "national"));
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX Ung", "ung"));
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX Gammel", "gammel"));
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX Sporty", "sport"));
            var channelList = channelRepo.ReadAll();

            // CommercialProduct
            var productRepo = new LinearDatabase<LinearSalesProduct>(dataDirectoryName);
            productRepo.DeleteAll();
            productRepo.Create(new LinearSalesProduct(Guid.NewGuid().ToString(), "classic 2-1", "2 parts exposure, 1 part specific"));
            productRepo.Create(new LinearSalesProduct(Guid.NewGuid().ToString(), "exposure", ""));
            productRepo.Create(new LinearSalesProduct(Guid.NewGuid().ToString(), "specific", ""));
            var productList = productRepo.ReadAll();

            // Agency
            LinearDatabase<LinearAgency> agencyRepo = new LinearDatabase<LinearAgency>(dataDirectoryName);
            agencyRepo.DeleteAll();
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "TVX INTERN", "1", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "Bling International", "3", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "Hansens Reklameagentur", "3", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "B-UNIQ", "2", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "AD Vantage", "3", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "MicroMacro", "1", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "SKUB agentur", "1", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "Belmonte", "1", false));
            IList<LinearAgency> agencyList = agencyRepo.ReadAll();

            // Users
            LinearDatabase<LinearUser> userRepo = new LinearDatabase<LinearUser>(dataDirectoryName);
            userRepo.DeleteAll();
            userRepo.Create(new LinearUser("78d7743d-d607-46ce-9767-0d57f5e1ef84", "Ada Adminsen", "adad", "adad@tvx.dk", agencyList.ElementAt(0).Id, true, true));
            userRepo.Create(new LinearUser("e167d15c-717f-4e9d-b2df-60a1b5af101c", "Eva de Bureau", "edb", "edb@bureau.net", agencyList.ElementAt(1).Id, true, true));
            userRepo.Create(new LinearUser("21a38b69-9224-48ee-8654-9608abe39bd7", "Bo Hansen", "boha", "boha@hansens.com", agencyList.ElementAt(2).Id, true, true));
            userRepo.Create(new LinearUser("bef3e140-2ce5-4c68-838c-399059fe4cad", "Julian Noah Gärtmütter", "jung", "jung@b-uniq.com", agencyList.ElementAt(3).Id, true, true));
            var userList = userRepo.ReadAll();

            // Advertisers
            IList<LinearAdvertiser> advertisers = new List<LinearAdvertiser>();
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "TestAnnoncøren", agencyList.ElementAt(0).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Sodavandsfabrikken A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Smukke Smykker Til Alle A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Tandklinikken Whiteout A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Filur & Co legetøj A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Danske Mejerier A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "HyperState Partyplanners A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Beaujoulais Vine A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "A.K. Bygmand A/S", agencyList.ElementAt(1).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Det Lækre Brød A/S", agencyList.ElementAt(2).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Fisker Find A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Fancy Party & udklædning A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Restaurant Bacchus A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Milli Vanilli Babytøj A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "FixDinI-Phone.net A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "RollespilsCenter Avedøre A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Belzebub Online Casino A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Økobryggeriet Sydhavn A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Toxic Arbejdssko A/S", agencyList.ElementAt(3).Id));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Dortes Rejser A/S", agencyList.ElementAt(3).Id));

            var advertiserRepo = new LinearDatabase<LinearAdvertiser>(dataDirectoryName);
            advertiserRepo.DeleteAll();

            foreach (var advertiser in advertisers)
            {
                advertiserRepo.Create(advertiser);
            }

            // Spot
            var spotList = new List<LinearSpot>();
            for (int i = 1; i < 32; i++)
            {
                spotList.Add(new LinearSpot(Guid.NewGuid().ToString(), new DateTime(2023, 1, i, 14, 00, 00), 0, channelList.ElementAt(0).Id, channelList.ElementAt(0).Name, "Fluefiske med Svend"));
                spotList.Add(new LinearSpot(Guid.NewGuid().ToString(), new DateTime(2023, 1, i, 15, 00, 00), 0, channelList.ElementAt(0).Id, channelList.ElementAt(0).Name, "Madkontoret"));
                spotList.Add(new LinearSpot(Guid.NewGuid().ToString(), new DateTime(2023, 1, i, 16, 30, 00), 0, channelList.ElementAt(0).Id, channelList.ElementAt(0).Name, "Mord i Oxford"));
                spotList.Add(new LinearSpot(Guid.NewGuid().ToString(), new DateTime(2023, 1, i, 17, 15, 00), 0, channelList.ElementAt(0).Id, channelList.ElementAt(0).Name, "Livet på slottet"));
            }
            var spotRepo = new LinearDatabase<LinearSpot>(dataDirectoryName);
            spotRepo.DeleteAll();
            spotRepo.CreateList(spotList);

            #endregion

            #region Order
            // Orders
            var orders = new List<LinearOrder>();
            var orderRepo = new LinearDatabase<LinearOrder>(dataDirectoryName);
            orderRepo.DeleteAll();
            var random = new Random();
            foreach (var advertiser in advertisers)
            {
                var agency = agencyRepo.Read(advertiser.Agency);
                if (agency == null) throw new Exception("Could not generate mock data because agency was not found");
                List<LinearUser>? agencyUsers = new();

                foreach (var user in userList)
                {
                    if (user.AgencyId == agency.Id)
                    {
                        agencyUsers.Add(user);
                    }
                }
                if (agencyUsers.Count == 0) throw new Exception("Could not generate mock data: Found no users for agency " + agency.Name + " id: " + agency.Id);

                var orderAmount = random.Next(1, 30);
                var startWeek = random.Next(1, 8);

                for (int i = 0; i < orderAmount; i++)
                {
                    var order = new LinearOrder(
                        id: Guid.NewGuid().ToString(),
                        modifiedTime: DateTime.Now,
                        ordernumber: random.Next(1000000, 10000000).ToString(),
                        advertiserId: advertiser.Id,
                        advertiserName: advertiser.Name,
                        advertiserProductName: GenerateProductName(),
                        handlerId: agencyUsers.ElementAt(random.Next(agencyUsers.Count)).Id,
                        startWeek: startWeek,
                        endWeek: startWeek + random.Next(1, 4),
                        // Every 10th order is generated as specific order with specific salesproduct
                        orderTypeName: (i % 10 == 0) ? OrderTypeName.specific.ToString() : OrderTypeName.exposure.ToString(),
                        channelId: channelList.ElementAt(random.Next(channelList.Count)).Id,
                        salesProductId: (i % 10 == 0) ? productList.ElementAt(2).Id : productList.ElementAt(1).Id,
                        salesProductName: (i % 10 == 0) ? productList.ElementAt(2).Name : productList.ElementAt(1).Name,
                        salesGroupNumber: null,
                        durationSeconds: 30,
                        costPerMille: random.Next(50, 150),
                        viewsExpectedMille: random.Next(1, 100),
                        viewsDeliveredMille: 0,
                        orderStatus: OrderStatus.created.ToString(),
                        orderBudget: random.Next(200, 700) * 100, //CAst to double
                        orderTotal: 0
                    );

                    orders.Add(order);
                };
            }

            orderRepo.CreateList(orders);

            #endregion

            #region Spot Booking

            // Spot Booking
            var spotBookingRepo = new LinearDatabase<LinearSpotBooking>(dataDirectoryName);
            IList<LinearSpot> allSpots = spotRepo.ReadAll();
            IList<LinearSpot> updatedSpots = new List<LinearSpot>();
            var  spotbookings = new List<LinearSpotBooking>();

            foreach (var order in orders)
            {  
                if (order.OrderTypeName.Equals(OrderTypeName.specific.ToString()))
                {
                    var spot = GetFirstFreeSpot(spotList);
                    if (spot == null) throw new Exception("Need more spots than orders when generating data.");

                    spot.BookedSeconds += order.DurationSeconds; 
                    updatedSpots.Add(spot);
                    spotbookings.Add(new LinearSpotBooking(Guid.NewGuid().ToString(), spot.Id, order.Id));
                }
            }

            spotRepo.CreateList(updatedSpots); // Updates existing spots
            spotBookingRepo.CreateList(spotbookings);

            #endregion
        }

        #region Helper Methods

        private static LinearSpot? GetFirstFreeSpot(List<LinearSpot> spots) {
            foreach (var spot in spots) {
                if (spot.Duration - spot.BookedSeconds >= 30) return spot; 
            }
            return null;
        }

        private static string GenerateProductName()
        {
            var random = new Random();
            var word1 = CampaignWords.ElementAt(random.Next(CampaignWords.Count));
            var word2 = CampaignWords.ElementAt(random.Next(CampaignWords.Count));
            return word1 + " " + word2;
        }

        private static List<string> GetCampaignWords()
        {
            var jsonData = new LinearFileHandler("Text/").ReadAsString("CampaignWords");
            if (jsonData == null) throw new Exception("Could not fint text file for CampaignWords.");
            var data = JsonSerializer.Deserialize<List<string>>(jsonData);
            if (data == null) throw new Exception("Could not deserialize CampaignWords.");
            return data;
        }
        #endregion
    }
}
