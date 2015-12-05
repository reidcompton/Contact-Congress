using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
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
    }
}
