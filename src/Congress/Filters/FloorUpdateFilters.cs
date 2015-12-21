using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress.FilterBy
{
    public class FloorUpdate : BasicRequest
    {
        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("timestamp")]
        public DateFilter Timestamp { get; set; }

        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("year")]
        public IntFilter Year { get; set; }

        [JsonProperty("legislative_day")]
        public DateFilter LegislativeDay { get; set; }

        [JsonProperty("bill_ids")]
        public StringFilter[] BillIds { get; set; }

        [JsonProperty("roll_ids")]
        public StringFilter[] Rollids { get; set; }

        [JsonProperty("legislator_ids")]
        public StringFilter[] LegislatorIds { get; set; }
    }
}
