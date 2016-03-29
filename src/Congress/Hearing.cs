using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace SunlightCongress
{
    public class Hearings : SunlightData<Hearing>
    {
        public Hearings(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Hearings(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Hearing : BasicRequest
    {
        //queryable fields
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

        // non-queryable fields
        [JsonProperty("room")]
        private string Room { get; set; }

        [JsonProperty("description")]
        private string Description { get; set; }

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("committee")]
        private CommitteeDetails Committee { get; set; }

        [JsonProperty("witnesses")]
        private Witness[] Witnesses { get; set; }
    }

    public class Witness
    {
        [JsonProperty("first_name")]
        private string FirstName { get; set; }

        [JsonProperty("last_name")]
        private string LastName { get; set; }

        [JsonProperty("middle_name")]
        private string MiddleName { get; set; }

        [JsonProperty("organization")]
        private string Organization { get; set; }

        [JsonProperty("position")]
        private string Position { get; set; }

        [JsonProperty("witness_type")]
        private string WitnessType { get; set; }
        
        [JsonProperty("documents")]
        private HearingDocument[] Documents { get; set; }

        [JsonProperty("meeting_documents")]
        private MeetingDocument[] MeetingDocuments { get; set; }

        [JsonProperty("hearing_id")]
        private string HearingId { get; set; }

        [JsonProperty("house_event_id")]
        private string HouseEventId { get; set; }
    }

    public class MeetingDocument
    {
        [JsonProperty("description")]
        private string Description { get; set; }

        [JsonProperty("published_at")]
        private DateTime? PublishedAt { get; set; }

        [JsonProperty("type")]
        private string Type { get; set; }

        [JsonProperty("version_code")]
        private string VersionCode { get; set; }

        [JsonProperty("bioguide_id")]
        private string BioGuideId { get; set; }

        [JsonProperty("bill_id")]
        private string BillId { get; set; }

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("permalink")]
        private string Permalink { get; set; }
    }

    public class HearingDocument
    {
        [JsonProperty("description")]
        private string Description { get; set; }

        [JsonProperty("published_at")]
        private DateTime? PublishedAt { get; set; }

        [JsonProperty("type")]
        private string Type { get; set; }

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("permalink")]
        private string Permalink { get; set; }
    }

    public class CommitteeDetails
    {
        [JsonProperty("address")]
        private string Address { get; set; }

        [JsonProperty("chamber")]
        private string Chamber { get; set; }

        [JsonProperty("committee_id")]
        private string CommitteeId { get; set; }

        [JsonProperty("name")]
        private string Name { get; set; }

        [JsonProperty("office")]
        private string Office { get; set; }

        [JsonProperty("phone")]
        private string Phone { get; set; }

        [JsonProperty("subcommittee")]
        private bool? Subcommittee { get; set; }
    }
}
