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

    public class Page
    {
        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        [JsonProperty("page")]
        public int? PageNumber { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }
    }
   
    public interface Filter<T>
    {
        T[] Values { get; set; }
        bool? Not { get; set; }
        bool? All { get; set; }
        bool? In { get; set; }
        bool? NotIn { get; set; }
        bool? Exists { get; set; }
    }

    public class DateTimeFilter : Filter<DateTime>
    {
        public DateTimeFilter(DateTime value) { Values = new DateTime[] { new DateTime(value.Ticks) }; }
        public DateTimeFilter(DateTime[] values) { Values = values; }
        public DateTimeFilter(DateTime value, Options modifier)
        {
            Values = new DateTime[] { new DateTime(value.Ticks) };
            switch (modifier)
            {
                case Options.GreaterThan:
                    GreaterThan = true;
                    break;
                case Options.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Options.LessThan:
                    LessThan = true;
                    break;
                case Options.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Options.Not:
                    Not = true;
                    break;
                case Options.All:
                    All = true;
                    break;
                case Options.In:
                    In = true;
                    break;
                case Options.NotIn:
                    NotIn = true;
                    break;
                case Options.Exists:
                    Exists = true;
                    break;
            }
        }
        public DateTimeFilter(DateTime[] values, Options modifier)
        {
            Values = values;
            switch(modifier)
            {
                case Options.GreaterThan:
                    GreaterThan = true;
                    break;
                case Options.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Options.LessThan:
                    LessThan = true;
                    break;
                case Options.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Options.Not:
                    Not = true;
                    break;
                case Options.All:
                    All = true;
                    break;
                case Options.In:
                    In = true;
                    break;
                case Options.NotIn:
                    NotIn = true;
                    break;
                case Options.Exists:
                    Exists = true;
                    break;
            }
        }

        public DateTime[] Values { get; set; }
        public bool? GreaterThan { get; set; }
        public bool? GreaterThanOrEquals { get; set; }
        public bool? LessThan { get; set; }
        public bool? LessThanOrEquals { get; set; }
        public bool? Not { get; set; }
        public bool? All { get; set; }
        public bool? In { get; set; }
        public bool? NotIn { get; set; }
        public bool? Exists { get; set; }

        public enum Options
        {
            GreaterThan,
            GreaterThanOrEquals,
            LessThan,
            LessThanOrEquals,
            Not,
            All,
            In,
            NotIn,
            Exists
        }
    }

    public class IntFilter : Filter<int>
    {
        public IntFilter(int value) { Values = new int[] { value }; }
        public IntFilter(int[] values) { Values = values; }
        public IntFilter(int value, Options modifier)
        {
            Values = new int[] { value };
            switch (modifier)
            {
                case Options.GreaterThan:
                    GreaterThan = true;
                    break;
                case Options.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Options.LessThan:
                    LessThan = true;
                    break;
                case Options.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Options.Not:
                    Not = true;
                    break;
                case Options.All:
                    All = true;
                    break;
                case Options.In:
                    In = true;
                    break;
                case Options.NotIn:
                    NotIn = true;
                    break;
                case Options.Exists:
                    Exists = true;
                    break;
            }
        }
        public IntFilter(int[] values, Options modifier)
        {
            Values = values;
            switch (modifier)
            {
                case Options.GreaterThan:
                    GreaterThan = true;
                    break;
                case Options.GreaterThanOrEquals:
                    GreaterThanOrEquals = true;
                    break;
                case Options.LessThan:
                    LessThan = true;
                    break;
                case Options.LessThanOrEquals:
                    LessThanOrEquals = true;
                    break;
                case Options.Not:
                    Not = true;
                    break;
                case Options.All:
                    All = true;
                    break;
                case Options.In:
                    In = true;
                    break;
                case Options.NotIn:
                    NotIn = true;
                    break;
                case Options.Exists:
                    Exists = true;
                    break;
            }
        }

        public int[] Values { get; set; }
        public bool? GreaterThan { get; set; }
        public bool? GreaterThanOrEquals { get; set; }
        public bool? LessThan { get; set; }
        public bool? LessThanOrEquals { get; set; }
        public bool? Not { get; set; }
        public bool? All { get; set; }
        public bool? In { get; set; }
        public bool? NotIn { get; set; }
        public bool? Exists { get; set; }

        public enum Options
        {
            GreaterThan,
            GreaterThanOrEquals,
            LessThan,
            LessThanOrEquals,
            Not,
            All,
            In,
            NotIn,
            Exists
        }
    }

    public class StringFilter : Filter<string>
    {
        public StringFilter(string value) { Values = new string[] { value }; }
        public StringFilter(string[] values) { Values = values; }
        public StringFilter(string value, Options modifier)
        {
            Values = new string[] { value };
            switch (modifier)
            {
                case Options.Not:
                    Not = true;
                    break;
                case Options.All:
                    All = true;
                    break;
                case Options.In:
                    In = true;
                    break;
                case Options.NotIn:
                    NotIn = true;
                    break;
                case Options.Exists:
                    Exists = true;
                    break;
            }
        }
        public StringFilter(string[] values, Options modifier)
        {
            Values = values;
            switch (modifier)
            {
                case Options.Not:
                    Not = true;
                    break;
                case Options.All:
                    All = true;
                    break;
                case Options.In:
                    In = true;
                    break;
                case Options.NotIn:
                    NotIn = true;
                    break;
                case Options.Exists:
                    Exists = true;
                    break;
            }
        }

        public string[] Values { get; set; }
        public bool? Not { get; set; }
        public bool? All { get; set; }
        public bool? In { get; set; }
        public bool? NotIn { get; set; }
        public bool? Exists { get; set; }

        public enum Options
        {
            Not,
            All,
            In,
            NotIn,
            Exists
        }
    }
}
