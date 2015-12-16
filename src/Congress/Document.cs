using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Congress
{
    public class DocumentWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<Document> Results { get; set; }
    }

    public class Document
    {
        [JsonProperty("document_type")]
        public string DocumentType { get; set; }

        [JsonProperty("document_type_name")]
        public string DocumentTypeName { get; set; }

        [JsonProperty("posted_at")]
        public DateTime? PostedAt { get; set; }

        [JsonProperty("published_on")]
        public DateTime? PublishedOn { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("categories")]
        public string[] Categories { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("gao_reports")]
        public GaoReport[] GaoReports { get; set; }

        public static List<Document> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.DocumentsSearchUrl, Settings.Token);
            return Helpers.Get<DocumentWrapper>(url).Results;
        }
    }

    public class IgReport
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("inspector_url")]
        public string InspectorUrl { get; set; }

        [JsonProperty("inspector")]
        public string Inspector { get; set; }

        [JsonProperty("pdf")]
        public Pdf Pdf { get; set; }

        [JsonProperty("published_on")]
        public DateTime? PublishedOn { get; set; }

        [JsonProperty("agency")]
        public string Agency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("agency_name")]
        public string AgencyName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("file_type")]
        public string FileType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("report_id")]
        public string ReportId { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }
    }

    public class Pdf
    {
        [JsonProperty("modification_date")]
        public DateTime? ModificationDate { get; set; }

        [JsonProperty("creation_date")]
        public DateTime? CreationDate { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("page_count")]
        public int? PageCount { get; set; }
    }

    public class GaoReport
    {
        [JsonProperty("gao_id")]
        public string GaoId { get; set; }

        [JsonProperty("report_number")]
        public string ReportNumber { get; set; }

        [JsonProperty("supplement_url")]
        public string SupplementUrl { get; set; }

        [JsonProperty("youtube_id")]
        public string YoutubeId { get; set; }

        [JsonProperty("links")]
        public string Links { get; set; }
    }
}
