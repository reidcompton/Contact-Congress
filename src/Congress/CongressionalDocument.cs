using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Congress
{
    public class CongressionalDocuments : SunlightData<CongressionalDocument>
    {
        public CongressionalDocuments(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public CongressionalDocuments(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class CongressionalDocument : BasicRequest
    {
        // non-queryable fields
        [JsonProperty("document_id")]
        private string DocumentId { get; set; }

        [JsonProperty("document_type")]
        private string DocumentType { get; set; }

        [JsonProperty("chamber")]
        private string Chamber { get; set; }

        [JsonProperty("committee_id")]
        private string CommitteeId { get; set; }

        [JsonProperty("committee_names")]
        private string[] CommitteeNames { get; set; }

        [JsonProperty("congress")]
        private int? Congress { get; set; }

        [JsonProperty("house_event_id")]
        private string HouseEventId { get; set; }

        [JsonProperty("hearing_type_code")]
        private string HearingTypeCode { get; set; }

        [JsonProperty("hearing_title")]
        private string HearingTitle { get; set; }

        [JsonProperty("published_at")]
        private DateTime? PublishedAt { get; set; }

        [JsonProperty("bill_id")]
        private string BillId { get; set; }

        [JsonProperty("description")]
        private string Description { get; set; }

        [JsonProperty("version_code")]
        private string VersionCode { get; set; }

        [JsonProperty("bioguide_id")]
        private string BioGuideId { get; set; }

        [JsonProperty("occurs_at")]
        private DateTime? OccursAt { get; set; }

        [JsonProperty("urls")]
        private CongressionalDocumentUrl[] Urls { get; set; }

        [JsonProperty("text")]
        private string Text { get; set; }

        [JsonProperty("text_preview")]
        private string TextPreview { get; set; }

        [JsonProperty("witness")]
        private CongressionalDocumentWitness Witness { get; set; }
    }

    public class CongressionalDocumentWitness
    {

        [JsonProperty("position")]
        private string Position { get; set; }

        [JsonProperty("witness_type")]
        private string WitnessType { get; set; }

        [JsonProperty("first_name")]
        private string FirstName { get; set; }

        [JsonProperty("organization")]
        private string Organization { get; set; }

        [JsonProperty("middle_name")]
        private string MiddleName { get; set; }

        [JsonProperty("last_name")]
        private string LastName { get; set; }
    }

    public class CongressionalDocumentUrl
    {
        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("permalink")]
        private string Permalink { get; set; }
    }
}
