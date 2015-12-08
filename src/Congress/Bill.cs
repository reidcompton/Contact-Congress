using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
    public class BillWrapper
    {
        [JsonProperty("results")]
        public List<Bill> Results { get; set; }
    }

    public class Bill : BillFilters
    {
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

        public class Filters : BillFilters { }

        public static List<Bill> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.BillsUrl, Settings.Token);
            return Helpers.Get<BillWrapper>(url).Results;
        }

        public static List<Bill> Filter(Bill.Filters filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.BillsUrl, Settings.Token);
            return Helpers.Get<BillWrapper>(Helpers.QueryString(url, filters)).Results;
        }

        public static List<Bill> Search(string query, Bill.Filters filters)
        {
            string url = string.Format("{0}?apikey={1}&query={2}", Settings.BillsSearchUrl, Settings.Token, query);
            return Helpers.Get<BillWrapper>(Helpers.QueryString(url, filters)).Results;
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
}
