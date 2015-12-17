using Newtonsoft.Json;

namespace Congress
{
    public class VoteFilter : BasicRequest
    {
        [JsonProperty("roll_id")]
        public StringFilter RollId { get; set; }

        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("number")]
        public IntFilter Number { get; set; }

        [JsonProperty("year")]
        public IntFilter Year { get; set; }

        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("voted_at")]
        public DateTimeFilter VotedAt { get; set; }

        [JsonProperty("vote_type")]
        public StringFilter VoteType { get; set; }

        [JsonProperty("roll_type")]
        public StringFilter RollType { get; set; }

        [JsonProperty("required")]
        public StringFilter Required { get; set; }

        [JsonProperty("result")]
        public StringFilter Result { get; set; }

        [JsonProperty("bill_id")]
        public StringFilter BillId { get; set; }

        [JsonProperty("nomination_id")]
        public StringFilter NominationId { get; set; }

        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; }
    }
}
