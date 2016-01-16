using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress.FilterBy
{
    public class Amendment : BasicRequest
    {
        [JsonProperty("amendment_id")]
        public StringFilter AmendmentId { get; set; }

        [JsonProperty("amendment_type")]
        public StringFilter AmendmentType { get; set; }

        [JsonProperty("number")]
        public IntFilter Number { get; set; }

        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("house_number")]
        public IntFilter HouseNumber { get; set; }

        [JsonProperty("amends_amendment_id")]
        public StringFilter AmendsAmendmentId { get; set; }

        [JsonProperty("amends_bill_id")]
        public StringFilter AmendsBillId { get; set; }

        [JsonProperty("sponsor_type")]
        public StringFilter SponsorType { get; set; }

        [JsonProperty("sponsor_id")]
        public StringFilter SponsorId { get; set; }

        [JsonProperty("introduced_on")]
        public DateFilter IntroducedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateFilter LastActionAt { get; set; }
    }
}
