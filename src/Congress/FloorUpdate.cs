using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
    public class FloorUpdateWrapper
    {
        [JsonProperty("results")]
        public List<FloorUpdate> Results { get; set; }
    }

    public class FloorUpdate : FloorUpdateFilters
    {
        [JsonProperty("update")]
        public string Update { get; set; }

        public class Filters : FloorUpdateFilters { }

        public static List<FloorUpdate> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.FloorUpdatesUrl, Settings.Token);
            return Helpers.Get<FloorUpdateWrapper>(url).Results;
        }

        public static List<FloorUpdate> Filter(FloorUpdate.Filters filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.FloorUpdatesUrl, Settings.Token);
            return Helpers.Get<FloorUpdateWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }
}
