using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace SunlightCongress
{
    public class Committees : SunlightData<Committee>
    {
        public Committees(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Committees(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Committee : BasicRequest
    {
        // queryable fields
        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("subcommittee")]
        public bool? SubCommittee { get; set; }

        [JsonProperty("member_ids")]
        public string[] MemberIds { get; set; }

        [JsonProperty("parent_committee_id")]
        public string ParentCommitteeId { get; set; }

        // non-queryable fields
        [JsonProperty("side")]
        private string Name { get; set; }        

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("office")]
        private string Office { get; set; }

        [JsonProperty("phone")]
        private string Phone { get; set; }

        [JsonProperty("members")]
        private Member[] Members { get; set; }

        [JsonProperty("subcommittes")]
        private SubCommittee[] SubCommittees { get; set; }

        [JsonProperty("parent_committee")]
        private ParentCommittee ParentCommittee { get; set; }
    }

    public class ParentCommittee
    {
        [JsonProperty("committee_id")]
        private string CommitteeId { get; set; }

        [JsonProperty("name")]
        private string Name { get; set; }

        [JsonProperty("chamber")]
        private string Chamber { get; set; }

        [JsonProperty("website")]
        private string Website { get; set; }

        [JsonProperty("office")]
        private string Office { get; set; }

        [JsonProperty("phone")]
        private string Phone { get; set; }
    }

    public class SubCommittee
    {
        [JsonProperty("side")]
        private string Name { get; set; }

        [JsonProperty("committee_id")]
        private string CommitteeId { get; set; }

        [JsonProperty("phone")]
        private string Phone { get; set; }

        [JsonProperty("chamber")]
        private string Chamber { get; set; }
    }

    public class Member
    {
        [JsonProperty("side")]
        private string Side { get; set; }

        [JsonProperty("rank")]
        private int? Rank { get; set; }

        [JsonProperty("title")]
        private string Title { get; set; }

        [JsonProperty("legislator")]
        private Legislator Legislator { get; set; }
    }
}
