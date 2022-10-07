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

            // Advertisers

            IList<LinearAdvertiser> advertisers = new List<LinearAdvertiser>();
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Sodavandsfabrikken A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Smukke Smykker Til Alle A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Tandklinikken Whiteout A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Filur & Co legetøj A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Danske Mejerier A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "HyperState Partyplanners A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Beaujoulais Vine A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "A.K. Bygmand A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Det Lækre Brød A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Fisker Find A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Fancy Party & udklædning A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Restaurant Bacchus A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Milli Vanilli Babytøj A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "FixDinI-Phone.net A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "RollespilsCenter Avedøre A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Belzebub Online Casino A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Økobryggeriet Sydhavn A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Toxic Arbejdssko A/S"));
            advertisers.Add(new LinearAdvertiser(Guid.NewGuid().ToString(), "Dortes Rejser A/S"));

            LinearRepo<LinearAdvertiser> advertiserRepo = new LinearRepo<LinearAdvertiser>(dataDirectoryName);
            foreach (var advertiser in advertisers)
            {
                advertiserRepo.Create(advertiser);
            }

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

            agencyList.ElementAt(0).Advertisers = advertisers.Select(x => x.Id).ToList(); // Take All
            agencyList.ElementAt(1).Advertisers = advertisers.Take(0..3).Select(x => x.Id).ToList();
            agencyList.ElementAt(2).Advertisers = advertisers.Take(3..10).Select(x => x.Id).ToList();
            agencyList.ElementAt(3).Advertisers = advertisers.Take(10..12).Select(x => x.Id).ToList();
            agencyList.ElementAt(4).Advertisers = advertisers.Take(12..14).Select(x => x.Id).ToList();
            agencyList.ElementAt(5).Advertisers = advertisers.Take(14..15).Select(x => x.Id).ToList();
            agencyList.ElementAt(6).Advertisers = advertisers.Take(15..18).Select(x => x.Id).ToList();
            agencyList.ElementAt(7).Advertisers = advertisers.Take(18..19).Select(x => x.Id).ToList();

            LinearRepo<LinearAgency> agencyRepo = new LinearRepo<LinearAgency>(dataDirectoryName);
            foreach (var agency in agencyList)
            {
                agencyRepo.Create(agency);
            }

            // Users

            LinearRepo<LinearUser> repo = new LinearRepo<LinearUser>(dataDirectoryName);
            repo.Create(new LinearUser("78d7743d-d607-46ce-9767-0d57f5e1ef84", "Adam Adminsen", "aadm", "aadm@tvx.dk", agencyList.ElementAt(0).Id, true, true));
            repo.Create(new LinearUser("e167d15c-717f-4e9d-b2df-60a1b5af101c", "Eva deBureau", "evad", "evad@bureau.net", agencyList.ElementAt(1).Id, true, true));
            repo.Create(new LinearUser("21a38b69-9224-48ee-8654-9608abe39bd7", "Bo Hansen", "boha", "boha@hansens.com", agencyList.ElementAt(2).Id, true, true));
            repo.Create(new LinearUser("bef3e140-2ce5-4c68-838c-399059fe4cad", "Julian André Gürtmitter", "jang", "jang@BUNIQ.com", agencyList.ElementAt(3).Id, true, true));

            // Orders

            // (Orderlines)

            // Channels

            // ChannelSplits

            // Targetgroups


        }



    }
}
