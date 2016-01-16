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

    public class UpcomingBill
    {
        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("source_type")]
        public string SourceType { get; set; }

        [JsonProperty("legislative_day")]
        public DateTime? LegislativeDay { get; set; }

        [JsonProperty("scheduled_at")]
        public DateTime? ScheduledAt { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("bill")]
        public Bill Bill { get; set; }

        public static List<UpcomingBill> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<UpcomingBillWrapper>(url).Results;
        }

        public static List<UpcomingBill> Search(FilterBy.UpcomingBill filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<UpcomingBillWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }
}
