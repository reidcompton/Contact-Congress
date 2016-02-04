using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Congress
{
    public class UpcomingBills : SunlightData<UpcomingBill>
    {
        public UpcomingBills(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public UpcomingBills(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class UpcomingBill : BasicRequest
    {
        // queryable fields
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

        // non-queryable fields
        [JsonProperty("context")]
        private string Context { get; set; }

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("bill")]
        private Bill Bill { get; set; }

    }
}
