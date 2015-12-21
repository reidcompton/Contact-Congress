using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress.FilterBy
{ 
    public class Committee : BasicRequest
    {
        [JsonProperty("committee_id")]
        public StringFilter CommitteeId { get; set; }

        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("subcommittee")]
        public bool? SubCommittee { get; set; }

        [JsonProperty("member_ids")]
        public StringFilter[] MemberIds { get; set; }

        [JsonProperty("parent_committee_id")]
        public StringFilter ParentCommitteeId { get; set; }
    }
}
