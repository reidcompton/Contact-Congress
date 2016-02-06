using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Congress
{
    public class Districts : SunlightData<District>
    {
        public Districts(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Districts(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class District
    {
        // queryable fields
        [JsonProperty("zip")]
        public int? Zip { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        // non-queryable fields
        [JsonProperty("state")]
        private string State { get; set; }

        [JsonProperty("district")]
        private int? DistrictNumber { get; set; }
    }
}