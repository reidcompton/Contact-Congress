using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
    public class VoteFilter
    {
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
        public string BillID { get; set; }

        [JsonProperty("nomination_id")]
        public string NominationId { get; set; }

        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; }
    }
}
