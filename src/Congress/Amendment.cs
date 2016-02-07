using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Congress
{
    public class Amendments : SunlightData<Amendment>
    {
        public Amendments(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Amendments(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Amendment : BasicRequest
    {
        [JsonProperty("amendment_id")]
        public string AmendmentId { get; set; }

        [JsonProperty("amendment_type")]
        public string AmendmentType { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("house_number")]
        public int? HouseNumber { get; set; }

        [JsonProperty("amends_amendment_id")]
        public string AmendsAmendmentId { get; set; }

        [JsonProperty("amends_bill_id")]
        public string AmendsBillId { get; set; }

        [JsonProperty("sponsor_type")]
        public string SponsorType { get; set; }

        [JsonProperty("sponsor_id")]
        public string SponsorId { get; set; }

        [JsonProperty("introduced_on")]
        public DateTime? IntroducedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateTime? LastActionAt { get; set; }

        [JsonProperty("amends_amendment")]
        private Amendment AmendsAmendment { get; set; }

        [JsonProperty("amends_bill")]
        private Bill AmendsBill { get; set; }

        [JsonProperty("sponsor")]
        private Legislator Sponsor { get; set; }

        [JsonProperty("purpose")]
        private string Purpose { get; set; }

        [JsonProperty("description")]
        private string Description { get; set; }

        [JsonProperty("actions")]
        private Action[] Actions { get; set; }
    }
}
