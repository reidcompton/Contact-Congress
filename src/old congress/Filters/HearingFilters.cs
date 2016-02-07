using Newtonsoft.Json;

namespace Congress.FilterBy
{
    public class Hearing : BasicRequest
    {
        [JsonProperty("committee_id")]
        public StringFilter CommitteeId { get; set; }

        [JsonProperty("occurs_at")]
        public DateFilter OccursAt { get; set; }

        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("dc")]
        public bool? Dc { get; set; }

        [JsonProperty("bill_ids")]
        public StringFilter[] BillIds { get; set; }

        [JsonProperty("hearing_type")]
        public StringFilter HearingType { get; set; }
    }
}
