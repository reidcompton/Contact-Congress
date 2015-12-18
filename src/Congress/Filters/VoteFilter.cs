using Newtonsoft.Json;
using System;

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
        public BreakdownFilter Breakdown { get; set; }
    }

    public class BreakdownFilter
    {
        [JsonProperty("total")]
        public TotalFilter Total { get; set; }

        [JsonProperty("party")]
        public PartyFilter Party { get; set; }
    }

    public class TotalFilter
    {
        [JsonProperty("Yea")]
        public IntFilter Yea { get; set; }

        [JsonProperty("Nay")]
        public IntFilter Nay { get; set; }

        [JsonProperty("Not Voting")]
        public IntFilter NotVoting { get; set; }

        [JsonProperty("Present")]
        public IntFilter Present { get; set; }
    }

    public class PartyFilter
    {
        [JsonProperty("R")]
        public TotalFilter Republican { get; set; }

        [JsonProperty("D")]
        public TotalFilter Democrat { get; set; }

        [JsonProperty("I")]
        public TotalFilter Independent { get; set; }
    }
}
