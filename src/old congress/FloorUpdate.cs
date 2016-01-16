using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Congress
{
    public class FloorUpdateWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<FloorUpdate> Results { get; set; }
    }

    public class FloorUpdate
    {
        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("legislative_day")]
        public DateTime? LegislativeDay { get; set; }

        [JsonProperty("bill_ids")]
        public string[] BillIds { get; set; }

        [JsonProperty("roll_ids")]
        public string[] Rollids { get; set; }

        [JsonProperty("legislator_ids")]
        public string[] LegislatorIds { get; set; }

        [JsonProperty("update")]
        public string Update { get; set; }

        public static List<FloorUpdate> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.FloorUpdatesUrl, Settings.Token);
            return Helpers.Get<FloorUpdateWrapper>(url).Results;
        }

        public static List<FloorUpdate> Search(FilterBy.FloorUpdate filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.FloorUpdatesUrl, Settings.Token);
            return Helpers.Get<FloorUpdateWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }
}
