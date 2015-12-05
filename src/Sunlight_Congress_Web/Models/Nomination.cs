using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{ 
    public class Nomination
    {
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

        [JsonProperty("actions")]
        public NominationAction[] Actions { get; set; }

        [JsonProperty("last_action")]
        public NominationAction LastAction { get; set; }
    }

    public class NominationAction
    {
        [JsonProperty("acted_at")]
        public DateTime? ActedAt { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
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
}
