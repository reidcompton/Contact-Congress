using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Congress
{
    public class CongressionalDocumentWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<CongressionalDocument> Results { get; set; }
    }

    public class CongressionalDocument
    {
        [JsonProperty("document_id")]
        public string DocumentId { get; set; }

        [JsonProperty("document_type")]
        public string DocumentType { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("committee_names")]
        public string CommitteeNames { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("house_event_id")]
        public string HouseEventId { get; set; }

        [JsonProperty("hearing_type_code")]
        public string HearingTypeCode { get; set; }

        [JsonProperty("hearing_title")]
        public string HearingTitle { get; set; }

        [JsonProperty("published_at")]
        public DateTime? PublishedAt { get; set; }

        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("version_code")]
        public string VersionCode { get; set; }

        [JsonProperty("bioguide_id")]
        public string BioGuideId { get; set; }

        [JsonProperty("occurs_at")]
        public DateTime? OccursAt { get; set; }

        [JsonProperty("urls")]
        public CongressionalDocumentUrl Urls { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("text_preview")]
        public string TextPreview { get; set; }

        [JsonProperty("witness")]
        public CongressionalDocumentWitness Witness { get; set; }

        public static List<CongressionalDocument> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<CongressionalDocumentWrapper>(url).Results;
        }
    }

    public class CongressionalDocumentWitness
    {

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("witness_type")]
        public string WitnessType { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }

    public class CongressionalDocumentUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }
}
