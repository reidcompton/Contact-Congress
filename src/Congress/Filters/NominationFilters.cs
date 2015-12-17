using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress
{
    public class NominationFilters : BasicReponse
    {
        [JsonProperty("nomination_id")]
        public StringFilter NominationId { get; set; }

        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("number")]
        public StringFilter Number { get; set; }

        [JsonProperty("received_on")]
        public DateTimeFilter ReceivedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateTimeFilter LastActionAt { get; set; }

        [JsonProperty("organization")]
        public StringFilter Organization { get; set; }

        [JsonProperty("nominees")]
        public NomineeFilter[] Nominees { get; set; }

        [JsonProperty("committee_ids")]
        public StringFilter CommitteeIds { get; set; }
    }

    public class NomineeFilter
    {
        [JsonProperty("name")]
        public StringFilter Name { get; set; }

        [JsonProperty("position")]
        public StringFilter Position { get; set; }

        [JsonProperty("state")]
        public StringFilter State { get; set; }
    }
}
