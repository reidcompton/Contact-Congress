using Newtonsoft.Json;

namespace Congress
{
    public class HearingFilters
    {
        [JsonProperty("committee_id")]
        public StringFilter CommitteeId { get; set; }

        [JsonProperty("occurs_at")]
        public DateTimeFilter OccursAt { get; set; }

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
