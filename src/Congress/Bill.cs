using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Congress
{
    public class BillWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<Bill> Results { get; set; }
    }

    public class Bill
    {
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
        public string[] Nicknames { get; set; }

        [JsonProperty("keywords")]
        public string[] Keywords { get; set; }

        [JsonProperty("sponsor_id")]
        public string SponsorId { get; set; }

        [JsonProperty("cosponsor_ids")]
        public string[] CoSponsorIds { get; set; }

        [JsonProperty("cosponsors_count")]
        public int? CoSponsorsCount { get; set; }

        [JsonProperty("withdrawn_cosponsor_ids")]
        public string[] WithdrawnCoSponsorIds { get; set; }

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

        [JsonProperty("official_title")]
        public string OfficialTitle { get; set; }

        [JsonProperty("popular_title")]
        public string PopularTitle { get; set; }

        [JsonProperty("short_title")]
        public string ShortTitle { get; set; }

        [JsonProperty("titles")]
        public Title[] Titles { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("summary_short")]
        public string SummaryShort { get; set; }

        [JsonProperty("urls")]
        public Url Urls { get; set; }

        [JsonProperty("actions")]
        public Action[] Actions { get; set; }

        [JsonProperty("votes")]
        public Action[] Votes { get; set; }

        [JsonProperty("sponsor")]
        public Legislator Sponsor { get; set; }

        [JsonProperty("cosponsors")]
        public CoSponsors[] CoSponsors { get; set; }

        [JsonProperty("withdrawn_cosponsors")]
        public CoSponsors[] WithdrawnCoSponsors { get; set; }

        [JsonProperty("committees")]
        public BillCommittee[] Committees { get; set; }

        [JsonProperty("versions")]
        public Version[] Versions { get; set; }

        [JsonProperty("upcoming")]
        public Upcoming[] Upcoming { get; set; }

        public static List<Bill> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.BillsUrl, Settings.Token);
            return Helpers.Get<BillWrapper>(url).Results;
        }

        public static List<Bill> Search(FilterBy.Bill filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.BillsUrl, Settings.Token);
            return Helpers.Get<BillWrapper>(Helpers.QueryString(url, filters)).Results;
        }

        public static List<Bill> Search(string query, FilterBy.Bill filters = null)
        {
            string url = string.Format("{0}?apikey={1}&query={2}", Settings.BillsSearchUrl, Settings.Token, query);
            if (filters != null)
                url = Helpers.QueryString(url, filters);
            return Helpers.Get<BillWrapper>(url).Results;
        }
    }

    

    public class Upcoming
    {
        [JsonProperty("source_type")]
        public string SourceType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("legislative_day")]
        public DateTime? LegislativeDay { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }
    }

    public class Version
    {
        [JsonProperty("version_code")]
        public string VersionCode { get; set; }

        [JsonProperty("issued_on")]
        public DateTime? IssuedOn { get; set; }

        [JsonProperty("version_name")]
        public string VersionName { get; set; }

        [JsonProperty("bill_version_id")]
        public string BillVersionId { get; set; }

        [JsonProperty("urls")]
        public VersionUrl Urls { get; set; }
    }

    public class VersionUrl
    {
        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("pdf")]
        public string Pdf { get; set; }
    }

    public class BillCommittee
    {
        [JsonProperty("activity")]
        public string[] Activity { get; set; }

        [JsonProperty("committee")]
        public Committee Committee { get; set; }
    }
   
    public class CoSponsors
    {

        [JsonProperty("sponsored_on")]
        public DateTime SponsoredOn { get; set; }

        [JsonProperty("legislator")]
        public Legislator Legislator { get; set; }
    }
    
    public class Action
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("acted_at")]
        public DateTime? ActedAt { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("how")]
        public string How { get; set; }

        [JsonProperty("vote_type")]
        public string VoteType { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("roll_id")]
        public string RollId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("references")]
        public Reference[] References { get; set; }
    }

    public class Reference
    {
        [JsonProperty("reference")]
        public string ReferenceId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Url
    {
        [JsonProperty("congress")]
        public string Congress { get; set; }

        [JsonProperty("govtrack")]
        public string GovTrack { get; set; }

        [JsonProperty("opencongress")]
        public string OpenCongress { get; set; }
    }

    public class Title
    {
        [JsonProperty("as")]
        public string As { get; set; }

        [JsonProperty("title")]
        public string BillTitle { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
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
}
