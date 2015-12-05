using Sunlight_Congress_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
    public class Committee
    {
        [JsonProperty("side")]
        public string Name { get; set; }

        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("subcommittee")]
        public bool SubCommittee { get; set; }

        [JsonProperty("member_ids")]
        public string[] MemberIds { get; set; }

        [JsonProperty("members")]
        public Member[] Members { get; set; }

        [JsonProperty("subcommittes")]
        public SubCommittee[] SubCommittees { get; set; }

        [JsonProperty("parent_committee_id")]
        public string ParentCommitteeId { get; set; }

        [JsonProperty("parent_committee")]
        public ParentCommittee ParentCommittee { get; set; }
    }

    public class ParentCommittee
    {
        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    public class SubCommittee
    {
        [JsonProperty("side")]
        public string Name { get; set; }

        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }
    }

    public class Member
    {
        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("rank")]
        public int? Rank { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("legislator")]
        public Legislator Legislator { get; set; }
    }
}
