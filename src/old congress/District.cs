using Newtonsoft.Json;
using System.Collections.Generic;

namespace Congress
{
    public class DistrictWrapper : BasicReponse
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


        public static List<District> Search(int zip)
        {
            string url = string.Format("{0}?zip={1}&apikey={2}", Settings.DistrictsLocateUrl, zip, Settings.Token);
            return Helpers.Get<DistrictWrapper>(url).Results;
        }

        public static List<District> Search(double latitude, double longitude)
        {
            string url = string.Format("{0}?latitude={1}&longitude={2}&apikey={3}", Settings.DistrictsLocateUrl, latitude, longitude, Settings.Token);
            return Helpers.Get<DistrictWrapper>(url).Results;
        }
    }
}