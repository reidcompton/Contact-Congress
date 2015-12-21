using System.Collections.Generic;
using Newtonsoft.Json;

namespace Congress
{
    public class CommitteeWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<Committee> Results { get; set; }
    }

    public class Committee
    {
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

        [JsonProperty("side")]
        public string Name { get; set; }        

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("members")]
        public Member[] Members { get; set; }

        [JsonProperty("subcommittes")]
        public SubCommittee[] SubCommittees { get; set; }

        [JsonProperty("parent_committee")]
        public ParentCommittee ParentCommittee { get; set; }

        public static List<Committee> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.CommitteesUrl, Settings.Token);
            return Helpers.Get<CommitteeWrapper>(url).Results;
        }

        public static List<Committee> Search(FilterBy.Committee filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.CommitteesUrl, Settings.Token);
            return Helpers.Get<CommitteeWrapper>(Helpers.QueryString(url, filters)).Results;
        }
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
