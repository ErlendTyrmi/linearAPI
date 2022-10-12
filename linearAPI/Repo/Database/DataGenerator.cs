using linearAPI.Entities;
using System.Collections.Generic;

namespace linearAPI.Repo.Database
{
    /// <summary>
    /// Making Mock Data. Does not overwrite, so delete before if you need a reset.
    /// </summary>
    public class DataGenerator
    {
        private static string dataDirectoryName = "Generated/";

        public static void Generate()
        {

            // Agencies

            IList<LinearAgency> agencyList = new List<LinearAgency>();
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "TVX INTERN", "1", false));
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "Bling International", "3", false));
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "Hansens Reklameagentur", "3", false));
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "B-UNIQ", "2", false));
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "AD Vantage", "3", false));
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "MicroMacro", "1", false));
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "SKUB agentur", "1", false));
            agencyList.Add(new LinearAgency(Guid.NewGuid().ToString(), "Belmonte", "1", false));

            LinearRepo<LinearAgency> agencyRepo = new LinearRepo<LinearAgency>(dataDirectoryName);
            foreach (var agency in agencyList)
            {
                agencyRepo.Create(agency);
            }

            // Users

            LinearRepo<LinearUser> repo = new LinearRepo<LinearUser>(dataDirectoryName);
            repo.Create(new LinearUser("78d7743d-d607-46ce-9767-0d57f5e1ef84", "Ada Adminsen", "adad", "adad@tvx.dk", agencyList.ElementAt(0).Id, true, true));
            repo.Create(new LinearUser("e167d15c-717f-4e9d-b2df-60a1b5af101c", "Eva de Bureau", "edb", "edb@bureau.net", agencyList.ElementAt(1).Id, true, true));
            repo.Create(new LinearUser("21a38b69-9224-48ee-8654-9608abe39bd7", "Bo Hansen", "boha", "boha@hansens.com", agencyList.ElementAt(2).Id, true, true));
            repo.Create(new LinearUser("bef3e140-2ce5-4c68-838c-399059fe4cad", "Julian Noah Gärtmütter", "jung", "jung@b-uniq.com", agencyList.ElementAt(3).Id, true, true));

            // Orders

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

            LinearRepo<LinearAdvertiser> advertiserRepo = new LinearRepo<LinearAdvertiser>(dataDirectoryName);
            foreach (var advertiser in advertisers)
            {
                advertiserRepo.Create(advertiser);
            }

            // Orders

            // Channels
            var channelRepo = new LinearRepo<Channel>();
            channelRepo.Create(new Channel(Guid.NewGuid().ToString(), "TVX National", "national"));
            channelRepo.Create(new Channel(Guid.NewGuid().ToString(), "TVX Ung", "ung"));
            channelRepo.Create(new Channel(Guid.NewGuid().ToString(), "TVX Gammel", "gammel"));
            channelRepo.Create(new Channel(Guid.NewGuid().ToString(), "TVX Sporty", "sport"));

            // CommercialProduct
            var productRepo = new LinearRepo<SalesProduct>();
            productRepo.Create(new SalesProduct(Guid.NewGuid().ToString(), "classic 2-1", "2 parts exposure, 1 part specific"));
            productRepo.Create(new SalesProduct(Guid.NewGuid().ToString(), "exposure", ""));
            productRepo.Create(new SalesProduct(Guid.NewGuid().ToString(), "specific", ""));

            // Order

            // SpotBooking

            // Spot
            var spotRepo = new LinearRepo<Spot>()

        }



    }
}
