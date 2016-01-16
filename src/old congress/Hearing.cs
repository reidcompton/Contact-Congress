using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress
{
    public class HearingWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<Hearing> Results { get; set; }
    }

    public class Hearing
    {
        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("occurs_at")]
        public DateTime? OccursAt { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("dc")]
        public bool? Dc { get; set; }

        [JsonProperty("bill_ids")]
        public string[] BillIds { get; set; }

        [JsonProperty("hearing_type")]
        public string HearingType { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("committee")]
        public CommitteeDetails Committee { get; set; }

        [JsonProperty("witnesses")]
        public Witness[] Witnesses { get; set; }

        public static List<Hearing> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.HearingsUrl, Settings.Token);
            return Helpers.Get<HearingWrapper>(url).Results;
        }

        public static List<Hearing> Search(FilterBy.Hearing filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.HearingsUrl, Settings.Token);
            return Helpers.Get<HearingWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }

    public class Witness
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("witness_type")]
        public string WitnessType { get; set; }
        
        [JsonProperty("documents")]
        public HearingDocument[] Documents { get; set; }

        [JsonProperty("meeting_documents")]
        public MeetingDocument[] MeetingDocuments { get; set; }

        [JsonProperty("hearing_id")]
        public string HearingId { get; set; }

        [JsonProperty("house_event_id")]
        public string HouseEventId { get; set; }
    }

    public class MeetingDocument
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("published_at")]
        public DateTime? PublishedAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version_code")]
        public string VersionCode { get; set; }

        [JsonProperty("bioguide_id")]
        public string BioGuideId { get; set; }

        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }

    public class HearingDocument
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("published_at")]
        public DateTime? PublishedAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }

    public class CommitteeDetails
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("subcommittee")]
        public bool? Subcommittee { get; set; }
    }
}
