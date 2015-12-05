using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
    public class Vote
    {
        [JsonProperty("roll_id")]
        public string RollId { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("voted_at")]
        public DateTime? VotedAt { get; set; }

        [JsonProperty("vote_type")]
        public string VoteType { get; set; }

        [JsonProperty("roll_type")]
        public string RollType { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("required")]
        public string Required { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("bill_id")]
        public string BillID { get; set; }

        [JsonProperty("bill")]
        public Bill Bill { get; set; }

        [JsonProperty("nomination_id")]
        public string NominationId { get; set; }

        [JsonProperty("nomination")]
        public Nomination Nomination { get; set; }

        [JsonProperty("voter_ids")]
        public KeyValuePair<string, string> VoterIds { get; set; }

        [JsonProperty("voters")]
        public Voters Voters { get; set; }

        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; }


    }

    public class Breakdown
    {
        [JsonProperty("breakdown")]
        public Tuple<string, Total> Total { get; set; }

        [JsonProperty("party")]
        public Tuple<string, Total> Party { get; set; }
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

    public class Voters
    {
        public List<Tuple<string, Voter>> Voter { get; set; }
    }

    public class Voter
    {
        public string Vote { get; set; }
        public Legislator VoterInfo { get; set; }
    }
}
