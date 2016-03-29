using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace SunlightCongress
{
    public class Bills : SunlightData<Bill>
    {
        public Bills(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Bills(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Bill : BasicRequest
    {
        // queryable fields
        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("bill_type")]
        public string BillType { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("introduced_on")]
        public DateTime? IntroducedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateTime? LastActionAt { get; set; }

        [JsonProperty("last_vote_at")]
        public DateTime? LastVoteAt { get; set; }

        [JsonProperty("last_version_on")]
        public DateTime? LastVersionOn { get; set; }

        [JsonProperty("nicknames")]
        public string Nicknames { get; set; }

        [JsonProperty("keywords")]
        public string Keywords { get; set; }

        [JsonProperty("sponsor_id")]
        public string SponsorId { get; set; }

        [JsonProperty("cosponsor_ids")]
        public string[] CoSponsorIds { get; set; }

        [JsonProperty("cosponsors_count")]
        public int? CoSponsorsCount { get; set; }

        [JsonProperty("withdrawn_cosponsor_ids")]
        public string WithdrawnCoSponsorIds { get; set; }

        [JsonProperty("withdrawn_cosponsors_count")]
        public int? WithdrawnCoSponsorsCount { get; set; }

        [JsonProperty("committee_ids")]
        public string[] CommitteeIds { get; set; }

        [JsonProperty("related_bill_ids")]
        public string[] RelatedBillIds { get; set; }

        [JsonProperty("history")]
        public History History { get; set; }

        [JsonProperty("enacted_as")]
        public EnactedAs EnactedAs { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        // non-queryable fields
        [JsonProperty("official_title")]
        private string OfficialTitle { get; set; }

        [JsonProperty("popular_title")]
        private string PopularTitle { get; set; }

        [JsonProperty("short_title")]
        private string ShortTitle { get; set; }

        [JsonProperty("titles")]
        private Title[] Titles { get; set; }

        [JsonProperty("summary")]
        private string Summary { get; set; }

        [JsonProperty("summary_short")]
        private string SummaryShort { get; set; }

        [JsonProperty("urls")]
        private Url Urls { get; set; }

        [JsonProperty("actions")]
        private Action[] Actions { get; set; }

        [JsonProperty("votes")]
        private Action[] Votes { get; set; }

        [JsonProperty("sponsor")]
        public Legislator Sponsor { get; set; }

        [JsonProperty("cosponsors")]
        private CoSponsors[] CoSponsors { get; set; }

        [JsonProperty("withdrawn_cosponsors")]
        private CoSponsors[] WithdrawnCoSponsors { get; set; }

        [JsonProperty("committees")]
        private BillCommittee[] Committees { get; set; }

        [JsonProperty("versions")]
        private Version[] Versions { get; set; }

        [JsonProperty("upcoming")]
        private Upcoming[] Upcoming { get; set; }
    }

    public class History
    {
        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("active_at")]
        public DateTime? ActiveAt { get; set; }

        [JsonProperty("house_passage_result")]
        public string HousePassageResult { get; set; }

        [JsonProperty("house_passage_result_at")]
        public DateTime? HousePassageResultAt { get; set; }

        [JsonProperty("senate_cloture_result")]
        public string SenateClotureResult { get; set; }

        [JsonProperty("senate_cloture_result_at")]
        public DateTime? SenateClotureResultAt { get; set; }

        [JsonProperty("senate_passage_result")]
        public string SenatePassageResult { get; set; }

        [JsonProperty("senate_passage_result_at")]
        public DateTime? SenatePassageResultAt { get; set; }

        [JsonProperty("vetoed")]
        public bool? Vetoed { get; set; }

        [JsonProperty("awaiting_signature")]
        public bool? AwaitingSignature { get; set; }

        [JsonProperty("enacted")]
        public bool? Enacted { get; set; }

        [JsonProperty("enacted_at")]
        public DateTime? EnactedAt { get; set; }
    }

    public class EnactedAs
    {
        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("law_type")]
        public string LawType { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }
    }

    public class Upcoming
    {
        [JsonProperty("source_type")]
        private string SourceType { get; set; }

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("chamber")]
        private string Chamber { get; set; }

        [JsonProperty("congress")]
        private int? Congress { get; set; }

        [JsonProperty("range")]
        private string Range { get; set; }

        [JsonProperty("legislative_day")]
        private DateTime? LegislativeDay { get; set; }

        [JsonProperty("context")]
        private string Context { get; set; }
    }

    public class Version
    {
        [JsonProperty("version_code")]
        private string VersionCode { get; set; }

        [JsonProperty("issued_on")]
        private DateTime? IssuedOn { get; set; }

        [JsonProperty("version_name")]
        private string VersionName { get; set; }

        [JsonProperty("bill_version_id")]
        private string BillVersionId { get; set; }

        [JsonProperty("urls")]
        private VersionUrl Urls { get; set; }
    }

    public class VersionUrl
    {
        [JsonProperty("html")]
        private string Html { get; set; }

        [JsonProperty("pdf")]
        private string Pdf { get; set; }
    }

    public class BillCommittee
    {
        [JsonProperty("activity")]
        private string[] Activity { get; set; }

        [JsonProperty("committee")]
        private Committee Committee { get; set; }
    }
   
    public class CoSponsors
    {

        [JsonProperty("sponsored_on")]
        private DateTime SponsoredOn { get; set; }

        [JsonProperty("legislator")]
        private Legislator Legislator { get; set; }
    }
    
    public class Action
    {
        [JsonProperty("type")]
        private string Type { get; set; }

        [JsonProperty("acted_at")]
        private DateTime? ActedAt { get; set; }

        [JsonProperty("chamber")]
        private string Chamber { get; set; }

        [JsonProperty("how")]
        private string How { get; set; }

        [JsonProperty("vote_type")]
        private string VoteType { get; set; }

        [JsonProperty("result")]
        private string Result { get; set; }

        [JsonProperty("roll_id")]
        private string RollId { get; set; }

        [JsonProperty("text")]
        private string Text { get; set; }

        [JsonProperty("references")]
        private Reference[] References { get; set; }
    }

    public class Reference
    {
        [JsonProperty("reference")]
        private string ReferenceId { get; set; }

        [JsonProperty("type")]
        private string Type { get; set; }
    }

    public class Url
    {
        [JsonProperty("congress")]
        private string Congress { get; set; }

        [JsonProperty("govtrack")]
        private string GovTrack { get; set; }

        [JsonProperty("opencongress")]
        private string OpenCongress { get; set; }
    }

    public class Title
    {
        [JsonProperty("as")]
        private string As { get; set; }

        [JsonProperty("title")]
        private string BillTitle { get; set; }

        [JsonProperty("type")]
        private string Type { get; set; }
    }
}
