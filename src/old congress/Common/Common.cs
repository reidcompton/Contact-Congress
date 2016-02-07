using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress
{
    public class BasicReponse
    {
        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }

    }

    public class BasicRequest
    {
        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }

        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

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

    public class Operator
    {
        public static string GreaterThan = "gt=";
        public static string GreaterThanOrEquals = "gte=";
        public static string LessThan = "lt=";
        public static string LessThanOrEquals = "lte=";
        public static string Not = "not=";
        public static string All = "all=";
        public static string In = "in=";
        public static string NotIn = "nin=";
        public static string Exists = "exists=true";
        public static string NotExists = "exists=false";
    }

    public class Filter<T>
    {

        public T[] Values { get; set; }
        public string Operator { get; set; }
    }

    public class DateFilter : Filter<DateTime>
    {
        public DateFilter(DateTime value) { Values = new DateTime[] { new DateTime(value.Ticks) }; }
        public DateFilter(DateTime[] values) { Values = values; }
        public DateFilter(DateTime value, string @operator)
        {
            Values = new DateTime[] { new DateTime(value.Ticks) };
            Operator = @operator;
        }
        public DateFilter(DateTime[] values, string @operator)
        {
            Values = values;
            Operator = @operator;
        }
    }

    public class IntFilter : Filter<int>
    {
        public IntFilter(int value) { Values = new int[] { value }; }
        public IntFilter(int[] values) { Values = values; }
        public IntFilter(int value, string @operator)
        {
            Values = new int[] { value };
            Operator = @operator;
        }
        public IntFilter(int[] values, string @operator)
        {
            Values = values;
            Operator = @operator;
        }
    }

    public class StringFilter : Filter<string>
    {
        public StringFilter(string value) { Values = new string[] { value }; }
        public StringFilter(string[] values) { Values = values; }
        public StringFilter(string value, string @operator)
        {
            Values = new string[] { value };
            Operator = @operator;
        }
        public StringFilter(string[] values, string @operator)
        {
            Values = values;
            Operator = @operator;
        }
    }
}
