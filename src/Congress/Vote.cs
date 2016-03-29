using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace SunlightCongress
{
    public class Votes : SunlightData<Vote>
    {
        public Votes(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Votes(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Vote : BasicRequest
    {
        // queryable fields
        [JsonProperty("roll_id")]
        public string RollId { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("voted_at")]
        public DateTime? VotedAt { get; set; }

        [JsonProperty("vote_type")]
        public string VoteType { get; set; }

        [JsonProperty("roll_type")]
        public string RollType { get; set; }

        [JsonProperty("required")]
        public string Required { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("nomination_id")]
        public string NominationId { get; set; }

        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; }


        // non-queryable fields
        [JsonProperty("question")]
        private string Question { get; set; }

        [JsonProperty("source")]
        private string Source { get; set; }

        [JsonProperty("bill")]
        private Bill Bill { get; set; }

        [JsonProperty("nomination")]
        private Nomination Nomination { get; set; }

        [JsonProperty("voter_ids")]
        private KeyValuePair<string, string> VoterIds { get; set; }

        [JsonProperty("voters")]
        private Voters Voters { get; set; }
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
        public int? Yea { get; set; }

        [JsonProperty("Nay")]
        public int? Nay { get; set; }

        [JsonProperty("Not Voting")]
        public int? NotVoting { get; set; }

        [JsonProperty("Present")]
        public int? Present { get; set; }
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
    
    public class Voters
    {
        private List<Tuple<string, Voter>> Voter { get; set; }
    }

    public class Voter
    {
        private string Vote { get; set; }
        private Legislator VoterInfo { get; set; }
    }
}
