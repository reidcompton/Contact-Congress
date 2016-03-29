using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SunlightCongress
{
    public class Documents : SunlightData<Document>
    {
        public Documents(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Documents(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Document
    {
        // non-queryable fields
        [JsonProperty("document_type")]
        private string DocumentType { get; set; }

        [JsonProperty("document_type_name")]
        private string DocumentTypeName { get; set; }

        [JsonProperty("posted_at")]
        private DateTime? PostedAt { get; set; }

        [JsonProperty("published_on")]
        private DateTime? PublishedOn { get; set; }

        [JsonProperty("title")]
        private string Title { get; set; }

        [JsonProperty("categories")]
        private string[] Categories { get; set; }

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("source_url")]
        private string SourceUrl { get; set; }

        [JsonProperty("gao_reports")]
        private GaoReport[] GaoReports { get; set; }
    }

    public class IgReport
    {
        [JsonProperty("topic")]
        private string Topic { get; set; }

        [JsonProperty("inspector_url")]
        private string InspectorUrl { get; set; }

        [JsonProperty("inspector")]
        private string Inspector { get; set; }

        [JsonProperty("pdf")]
        private Pdf Pdf { get; set; }

        [JsonProperty("published_on")]
        private DateTime? PublishedOn { get; set; }

        [JsonProperty("agency")]
        private string Agency { get; set; }

        [JsonProperty("type")]
        private string Type { get; set; }

        [JsonProperty("agency_name")]
        private string AgencyName { get; set; }

        [JsonProperty("url")]
        private string Url { get; set; }

        [JsonProperty("file_type")]
        private string FileType { get; set; }

        [JsonProperty("title")]
        private string Title { get; set; }

        [JsonProperty("report_id")]
        private string ReportId { get; set; }

        [JsonProperty("year")]
        private int? Year { get; set; }
    }

    public class Pdf
    {
        [JsonProperty("modification_date")]
        private DateTime? ModificationDate { get; set; }

        [JsonProperty("creation_date")]
        private DateTime? CreationDate { get; set; }

        [JsonProperty("author")]
        private string Author { get; set; }

        [JsonProperty("page_count")]
        private int? PageCount { get; set; }
    }

    public class GaoReport
    {
        [JsonProperty("gao_id")]
        private string GaoId { get; set; }

        [JsonProperty("report_number")]
        private string ReportNumber { get; set; }

        [JsonProperty("supplement_url")]
        private string SupplementUrl { get; set; }

        [JsonProperty("youtube_id")]
        private string YoutubeId { get; set; }

        [JsonProperty("links")]
        private string Links { get; set; }
    }
}
