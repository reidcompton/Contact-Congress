using System.Collections.Generic;
using Newtonsoft.Json;

namespace Congress
{
    public class AmendmentWrapper
    {
        [JsonProperty("results")]
        public List<Amendment> Results { get; set; }
    }
    
    public class Amendment : AmendmentFilters
    {
        [JsonProperty("amends_amendment")]
        public Amendment AmendsAmendment { get; set; }

        [JsonProperty("amends_bill")]
        public Bill AmendsBill { get; set; }

        [JsonProperty("sponsor")]
        public Legislator Sponsor { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("actions")]
        public Action[] Actions { get; set; }

        public class Filters : AmendmentFilters { }

        public static List<Amendment> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, Settings.Token);
            return Helpers.Get<AmendmentWrapper>(url).Results;
        }

        public static List<Amendment> Filter(Amendment.Filters filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, Settings.Token);
            return Helpers.Get<AmendmentWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }
}
