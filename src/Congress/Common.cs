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

    public enum Operator
    {
        GreaterThan,
        GreaterThanOrEquals,
        LessThan,
        LessThanOrEquals,
        Not,
        All,
        In,
        NotIn,
        Exists,
        NotExists
    }

    public class Filter<T>
    {

        public T[] Values { get; set; }
        public bool? GreaterThan { get; set; }
        public bool? GreaterThanOrEquals { get; set; }
        public bool? LessThan { get; set; }
        public bool? LessThanOrEquals { get; set; }
        public bool? Not { get; set; }
        public bool? All { get; set; }
        public bool? In { get; set; }
        public bool? NotIn { get; set; }
        public bool? Exists { get; set; }
    }

    public class DateFilter : Filter<DateTime>
    {
        public DateFilter(DateTime value) { Values = new DateTime[] { new DateTime(value.Ticks) }; }
        public DateFilter(DateTime[] values) { Values = values; }
        public DateFilter(DateTime value, Operator @operator)
        {
            Values = new DateTime[] { new DateTime(value.Ticks) };
            switch (@operator)
            {
                case Operator.GreaterThan:
                    GreaterThan = true;
                    break;
                case Operator.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Operator.LessThan:
                    LessThan = true;
                    break;
                case Operator.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Operator.Not:
                    Not = true;
                    break;
                case Operator.All:
                    All = true;
                    break;
                case Operator.In:
                    In = true;
                    break;
                case Operator.NotIn:
                    NotIn = true;
                    break;
                case Operator.Exists:
                    Exists = true;
                    break;
                case Operator.NotExists:
                    Exists = false;
                    break;
            }
        }
        public DateFilter(DateTime[] values, Operator @operator)
        {
            Values = values;
            switch(@operator)
            {
                case Operator.GreaterThan:
                    GreaterThan = true;
                    break;
                case Operator.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Operator.LessThan:
                    LessThan = true;
                    break;
                case Operator.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Operator.Not:
                    Not = true;
                    break;
                case Operator.All:
                    All = true;
                    break;
                case Operator.In:
                    In = true;
                    break;
                case Operator.NotIn:
                    NotIn = true;
                    break;
                case Operator.Exists:
                    Exists = true;
                    break;
                case Operator.NotExists:
                    Exists = false;
                    break;
            }
        }
    }

    public class IntFilter : Filter<int>
    {
        public IntFilter(int value) { Values = new int[] { value }; }
        public IntFilter(int[] values) { Values = values; }
        public IntFilter(int value, Operator @operator)
        {
            Values = new int[] { value };
            switch (@operator)
            {
                case Operator.GreaterThan:
                    GreaterThan = true;
                    break;
                case Operator.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Operator.LessThan:
                    LessThan = true;
                    break;
                case Operator.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Operator.Not:
                    Not = true;
                    break;
                case Operator.All:
                    All = true;
                    break;
                case Operator.In:
                    In = true;
                    break;
                case Operator.NotIn:
                    NotIn = true;
                    break;
                case Operator.Exists:
                    Exists = true;
                    break;
                case Operator.NotExists:
                    Exists = false;
                    break;
            }
        }
        public IntFilter(int[] values, Operator @operator)
        {
            Values = values;
            switch (@operator)
            {
                case Operator.GreaterThan:
                    GreaterThan = true;
                    break;
                case Operator.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Operator.LessThan:
                    LessThan = true;
                    break;
                case Operator.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Operator.Not:
                    Not = true;
                    break;
                case Operator.All:
                    All = true;
                    break;
                case Operator.In:
                    In = true;
                    break;
                case Operator.NotIn:
                    NotIn = true;
                    break;
                case Operator.Exists:
                    Exists = true;
                    break;
                case Operator.NotExists:
                    Exists = false;
                    break;
            }
        }        
    }

    public class StringFilter : Filter<string>
    {
        public StringFilter(string value) { Values = new string[] { value }; }
        public StringFilter(string[] values) { Values = values; }
        public StringFilter(string value, Operator @operator)
        {
            Values = new string[] { value };
            switch (@operator)
            {
                case Operator.Not:
                    Not = true;
                    break;
                case Operator.All:
                    All = true;
                    break;
                case Operator.In:
                    In = true;
                    break;
                case Operator.NotIn:
                    NotIn = true;
                    break;
                case Operator.Exists:
                    Exists = true;
                    break;
                case Operator.NotExists:
                    Exists = false;
                    break;
            }
        }
        public StringFilter(string[] values, Operator @operator)
        {
            Values = values;
            switch (@operator)
            {
                case Operator.Not:
                    Not = true;
                    break;
                case Operator.All:
                    All = true;
                    break;
                case Operator.In:
                    In = true;
                    break;
                case Operator.NotIn:
                    NotIn = true;
                    break;
                case Operator.Exists:
                    Exists = true;
                    break;
                case Operator.NotExists:
                    Exists = false;
                    break;
            }
        }
    }
}
