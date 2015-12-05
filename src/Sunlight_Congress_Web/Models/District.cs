using Newtonsoft.Json;
using Sunlight_Congress_Web.Models;
using System.Collections.Generic;

namespace Sunlight_Congress
{
    public class DistrictWrapper
    {
        [JsonProperty("results")]
        public List<District> Results { get; set; }
    }

    public class District
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("district")]
        public int? DistrictNumber { get; set; }


        public static List<District> Locate(int zip)
        {
            string url = string.Format("{0}?zip={1}&apikey={2}", Settings.DistrictsLocateUrl, zip, Settings.Token);
            return Helpers.Get<DistrictWrapper>(url).Results;
        }

        public static List<District> Locate(double latitude, double longitude)
        {
            string url = string.Format("{0}?latitude={1}&longitude={2}&apikey={3}", Settings.DistrictsLocateUrl, latitude, longitude, Settings.Token);
            return Helpers.Get<DistrictWrapper>(url).Results;
        }
    }
}