using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress
{
    public class NominationFilters
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
    }
}
