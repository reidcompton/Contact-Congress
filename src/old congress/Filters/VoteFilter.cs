using Newtonsoft.Json;
using System;

namespace Congress.FilterBy
{
    public class Vote : BasicRequest
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
        public DateFilter VotedAt { get; set; }

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

    public class Breakdown
    {
        [JsonProperty("total")]
        public Total Total { get; set; }

        [JsonProperty("party")]
        public Party Party { get; set; }
    }

    public class Total
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

    public class Party
    {
        [JsonProperty("R")]
        public Total Republican { get; set; }

        [JsonProperty("D")]
        public Total Democrat { get; set; }

        [JsonProperty("I")]
        public Total Independent { get; set; }
    }
}
