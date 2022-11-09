using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
using LinearUtils.Util;
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
using static LinearUtils.Util.Enums;

namespace LinearMockDatabase.Database
{
    /// <summary>
    /// Making Mock Data. Overwrites existing data.
    /// </summary>
    public class DataGenerator
    {
        private readonly static string dataDirectoryName = "Generated/";
        private readonly static  List<string> CampaignWords = GetCampaignWords();
        private readonly static  Random random = new();

        public static void Generate()
        {
            // This data does not change
            #region StaticData

            // Channels
            var channelRepo = new LinearAccess<LinearChannel>(dataDirectoryName);
            channelRepo.DeleteAll();
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX National", "national"));
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX Ung", "ung"));
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX Gammel", "gammel"));
            channelRepo.Create(new LinearChannel(Guid.NewGuid().ToString(), "TVX Sporty", "sport"));
            var channelList = channelRepo.ReadAll();

            // CommercialProduct
            var productRepo = new LinearAccess<LinearSalesProduct>(dataDirectoryName);
            productRepo.DeleteAll();
            productRepo.Create(new LinearSalesProduct(Guid.NewGuid().ToString(), "classic 2-1", "2 parts exposure, 1 part specific"));
            productRepo.Create(new LinearSalesProduct(Guid.NewGuid().ToString(), "exposure", ""));
            productRepo.Create(new LinearSalesProduct(Guid.NewGuid().ToString(), "specific", ""));
            var productList = productRepo.ReadAll();

            // Agency
            LinearAccess<LinearAgency> agencyRepo = new(dataDirectoryName);
            agencyRepo.DeleteAll();
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "TVX INTERN", "1", true));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "Bling International", "3", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "Hansens Reklameagentur", "3", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "B-UNIQ", "2", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "AD Vantage", "3", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "MicroMacro", "1", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "SKUB agentur", "1", false));
            agencyRepo.Create(new LinearAgency(Guid.NewGuid().ToString(), "Belmonte", "1", false));
            IList<LinearAgency> agencyList = agencyRepo.ReadAll();

            // Users
            LinearAccess<LinearUser> userRepo = new(dataDirectoryName);
            userRepo.DeleteAll();
            userRepo.Create(new LinearUser("78d7743d-d607-46ce-9767-0d57f5e1ef84", "Ada Adminsen", "adad", "adad@tvx.dk", agencyList.ElementAt(0).Id, true, true, true, true));
            userRepo.Create(new LinearUser("e167d15c-717f-4e9d-b2df-60a1b5af101c", "Eva de Bureau", "edb", "edb@bureau.net", agencyList.ElementAt(1).Id, true, true, true));
            userRepo.Create(new LinearUser("21a38b69-9224-48ee-8654-9608abe39bd7", "Bo Hansen", "boha", "boha@hansens.com", agencyList.ElementAt(2).Id, true, true, true));
            userRepo.Create(new LinearUser("bef3e140-2ce5-4c68-838c-399059fe4cad", "Julian Noah Gärtmütter", "jung", "jung@b-uniq.com", agencyList.ElementAt(3).Id, false, true, true, true));
            var userList = userRepo.ReadAll();

            // Advertisers
            var advertiserRepo = new LinearAccess<Advertiser>(dataDirectoryName);
            advertiserRepo.DeleteAll();
            IList<Advertiser> advertisers = new List<Advertiser>
            {
                new Advertiser(Guid.NewGuid().ToString(), "TVX TestAnnoncør", agencyList.ElementAt(0).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Sodavandsfabrikken A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Smukke Smykker Til Alle A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Tandklinikken Whiteout A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Filur & Co legetøj A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Danske Mejerier A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "HyperState Partyplanners A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Beaujoulais Vine A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "A.K. Bygmand A/S", agencyList.ElementAt(1).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Det Lækre Brød A/S", agencyList.ElementAt(2).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Fisker Find A/S", agencyList.ElementAt(2).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Fancy Party & udklædning A/S", agencyList.ElementAt(2).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Restaurant Bacchus A/S", agencyList.ElementAt(3).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Milli Vanilli Babytøj A/S", agencyList.ElementAt(3).Id),
                new Advertiser(Guid.NewGuid().ToString(), "FixDinI-Phone.net A/S", agencyList.ElementAt(3).Id),
                new Advertiser(Guid.NewGuid().ToString(), "RollespilsCenter Avedøre A/S", agencyList.ElementAt(3).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Belzebub Online Casino A/S", agencyList.ElementAt(3).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Økobryggeriet Sydhavn A/S", agencyList.ElementAt(3).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Toxic Arbejdssko A/S", agencyList.ElementAt(3).Id),
                new Advertiser(Guid.NewGuid().ToString(), "Dortes Rejser A/S", agencyList.ElementAt(3).Id)
            };

            foreach (var advertiser in advertisers)
            {
                advertiserRepo.Create(advertiser);
            }

            // Favorite advertisers
            var favoriteAdvertiserRepo = new LinearAccess<AdvertiserFavorites>(dataDirectoryName);
            favoriteAdvertiserRepo.DeleteAll();
            var favorites = new List<string>();

            var agencyId = agencyList.ElementAt(1).Id;
            var selectedAdvertisers = advertisers.Where((it) => it.AgencyId == agencyId).ToArray();
            var favoriteUser = userList.Where(it => it.AgencyId == agencyId).FirstOrDefault();
            if (favoriteUser == null)
            {
                throw new Exception("Datagenerator is missing a user for favorite advertisers");
            }
            else
            {
                favoriteAdvertiserRepo.Create(new AdvertiserFavorites(
                     favoriteUser.Id,
                    new string[2] { selectedAdvertisers.ElementAt(0).Id, selectedAdvertisers.ElementAt(1).Id })
                     );
            }



            // Spot
            var spotRepo = new LinearAccess<LinearSpot>(dataDirectoryName);
            spotRepo.DeleteAll();

            var titles = new List<string> { "Vidunderlige Slotte", "Havens Hemmeligheder", "Kontrolvers", "Hvem siger hvad?",
                "Det lille hus i Haderslev", "Til Søs", "Boldknold", "Sportens Verden", "Retrospekt", "Mord i Cambridge",
                "Det politiske hjørne", "Nuttede dyr", "Alma redder planeten", "Madkontoret", "De smukke drenge", "Alfreds gamle butik",
                "Rita tager Risikoen", "Mixifix!", "Bøllemosen - før og nu", "Den kinesiske forbindelse", "The 1%"};

            var spotList = new List<LinearSpot>();

            for (int day = 1; day < 32; day++)
            {
                titles.Shuffle();
                for (int titleIndex = 0; titleIndex < 19; titleIndex++)
                {
                    var hour = titleIndex + 5;
                    var channel = channelList.ElementAt(random.Next(channelList.Count));
                    spotList.Add(new LinearSpot(Guid.NewGuid().ToString(), new DateTime(2023, 1, day, hour, 00, 00), 0, channel.Id, channel.Name, titles.ElementAt(titleIndex)));
                }
            }
            // spotList.ForEach((spot) => { Console.WriteLine("On " + spot.ChannelName + " at " + spot.StartDateTime.Hour + ": " + spot.NextProgram); });
            spotRepo.CreateList(spotList);

            #endregion

            #region Order

            // Orders
            var orderRepo = new LinearAccess<LinearOrder>(dataDirectoryName);
            orderRepo.DeleteAll();
            var orders = new List<LinearOrder>();

            foreach (var advertiser in advertisers)
            {
                var agency = agencyRepo.Read(advertiser.AgencyId);
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

                var orderAmount = random.Next(1, 40);


                for (int i = 0; i < orderAmount; i++)
                {
                    var startWeek = random.Next(1, 6);
                    var specific = i % 4 == 0;

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

                        orderTypeName: specific ? OrderTypeName.specific.ToString() : OrderTypeName.exposure.ToString(),
                        channelId: channelList.ElementAt(random.Next(channelList.Count)).Id,
                        salesProductId: specific ? productList.ElementAt(2).Id : productList.ElementAt(1).Id,
                        salesProductName: specific ? productList.ElementAt(2).Name : productList.ElementAt(1).Name,
                        salesGroupNumber: null,
                        durationSeconds: 30,
                        costPerMille: random.Next(50, 150),
                        viewsExpectedMille: random.Next(1, 100),
                        viewsDeliveredMille: 0,
                        orderStatus: OrderStatus.created.ToString(),
                        orderBudget: random.Next(200, 700) * 100,
                        orderTotal: 0
                    );

                    orders.Add(order);
                };
            }

            orderRepo.CreateList(orders);

            #endregion

            #region Spot Booking

            // Spot Booking
            var spotBookingRepo = new LinearAccess<LinearSpotBooking>(dataDirectoryName);
            spotBookingRepo.DeleteAll();
            IList<LinearSpot> allSpots = spotRepo.ReadAll();
            IList<LinearSpot> updatedSpots = new List<LinearSpot>();
            var spotbookings = new List<LinearSpotBooking>();

            foreach (var order in orders)
            {
                if (order.OrderTypeName.Equals(OrderTypeName.specific.ToString()))
                {
                    var spot = GetAnyFreeSpot(allSpots);
                    if (spot == null) break;

                    spot.BookedSeconds += order.DurationSeconds;
                    updatedSpots.Add(spot);

                    var advertiser = advertiserRepo.Read(order.AdvertiserId);
                    if (advertiser == null) throw new Exception("Generator: Could not find advertiser " + order.AdvertiserId);

                    spotbookings.Add(new LinearSpotBooking(Guid.NewGuid().ToString(), spot.Id, order.Id, advertiser.AgencyId));
                }
            }

            if (updatedSpots.Count > 0)
            {
                spotRepo.CreateList(updatedSpots); // Updates existing spots
                spotBookingRepo.CreateList(spotbookings);
            }


            #endregion
        }

        #region Helper Methods

        private static LinearSpot? GetAnyFreeSpot(IList<LinearSpot> spots)
        {
            spots.Shuffle();
            foreach (var spot in spots)
            {
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
            if (jsonData == null) throw new Exception("Could not find text file for CampaignWords at ");
            var data = JsonSerializer.Deserialize<List<string>>(jsonData);
            if (data == null) throw new Exception("Could not deserialize CampaignWords.");
            return data;
        }



        #endregion
    }
}
