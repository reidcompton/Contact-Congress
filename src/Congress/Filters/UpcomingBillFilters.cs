using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress
{
    public class UpcomingBillFilters
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
        public DateTimeFilter LegislativeDay { get; set; }

        [JsonProperty("scheduled_at")]
        public DateTimeFilter ScheduledAt { get; set; }

        [JsonProperty("range")]
        public StringFilter Range { get; set; }
    }
}
