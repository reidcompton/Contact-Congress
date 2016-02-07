using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Congress
{
    public class Nominations : SunlightData<Nomination>
    {
        public Nominations(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Nominations(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Nomination : BasicRequest
    {
        // queryable fields
        [JsonProperty("nomination_id")]
        public string NominationId { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("received_on")]
        public DateTime? ReceivedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateTime? LastActionAt { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("nominees")]
        public Nominee[] Nominees { get; set; }

        [JsonProperty("committee_ids")]
        public string[] CommitteeIds { get; set; }

        // non-queryable fields
        [JsonProperty("actions")]
        private NominationAction[] Actions { get; set; }

        [JsonProperty("last_action")]
        private NominationAction LastAction { get; set; }
    }

    public class Nominee
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class NominationAction
    {
        [JsonProperty("acted_at")]
        private DateTime? ActedAt { get; set; }

        [JsonProperty("location")]
        private string Location { get; set; }
        
        [JsonProperty("text")]
        private string Text { get; set; }

        [JsonProperty("type")]
        private string Type { get; set; }
    }
}
