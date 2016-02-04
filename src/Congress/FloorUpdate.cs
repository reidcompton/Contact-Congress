using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Congress
{
    public class FloorUpdates : SunlightData<FloorUpdate>
    {
        public FloorUpdates(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public FloorUpdates(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class FloorUpdate : BasicRequest
    {
        // queryable fields
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

        // non-queryable fields
        [JsonProperty("update")]
        private string Update { get; set; }
    }

}
