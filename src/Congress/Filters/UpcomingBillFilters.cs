using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress.FilterBy
{
    public class UpcomingBill : BasicRequest
    {
        [JsonProperty("bill_id")]
        public StringFilter BillId { get; set; }

        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("source_type")]
        public StringFilter SourceType { get; set; }

        [JsonProperty("legislative_day")]
        public DateFilter LegislativeDay { get; set; }

        [JsonProperty("scheduled_at")]
        public DateFilter ScheduledAt { get; set; }

        [JsonProperty("range")]
        public StringFilter Range { get; set; }
    }
}
