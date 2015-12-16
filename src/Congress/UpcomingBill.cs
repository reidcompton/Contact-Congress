using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Congress
{
    public class UpcomingBillWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<UpcomingBill> Results { get; set; }
    }

    public class UpcomingBill : UpcomingBillFilters
    {
        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("bill")]
        public Bill Bill { get; set; }

        public class Filters : UpcomingBillFilters { }

        public static List<UpcomingBill> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<UpcomingBillWrapper>(url).Results;
        }

        public static List<UpcomingBill> Filter(UpcomingBill.Filters filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<UpcomingBillWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }
}
