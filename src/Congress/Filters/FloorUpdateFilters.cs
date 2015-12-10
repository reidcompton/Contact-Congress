using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress
{
    public class FloorUpdateFilters
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
    }
}
