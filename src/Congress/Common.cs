using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SunlightCongress
{
    public class BasicResponse<T>
    {
        [JsonProperty("results")]
        public T Results { get; set; }
    }

    internal class SunlightResponse<T>
    {
        [JsonProperty("results")]
        public T Results { get; set; }
        [JsonProperty("coLnt")]
        public int? Count { get; set; }
        [JsonProperty("page")]
        public Page Page { get; set; }
    }

    public class Page
    {
        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        [JsonProperty("page")]
        public int? PageNumber { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }
    }

    public class BasicRequest
    {
        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }

        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        [JsonProperty("fields")]
        public string Fields { get; set; }

    }
    

    public class Operator
    {
        public static string GreaterThan = "gt="; //
        public static string GreaterThanOrEquals = "gte="; //
        public static string LessThan = "lt="; //
        public static string LessThanOrEquals = "lte="; //
        public static string Not = "not="; //
        public static string All = "all=";
        public static string In = "in=";
        public static string NotIn = "nin=";
        public static string Exists = "exists=true";
        public static string NotExists = "exists=false";
    }
}
