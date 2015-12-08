using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sunlight_Congress
{

    public class LegislatorWrapper
    {
        [JsonProperty("results")]
        public List<Legislator> Results { get; set; }
    }

    public class Legislator : LegislatorFilters
    {

        [JsonProperty("facebook_id")]
        public string FacebookId { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("terms")]
        public Term[] Terms { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("youtube_id")]
        public string YouTubeId { get; set; }

        public class Filters : LegislatorFilters { }
        
        public static List<Legislator> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.LegislatorsUrl, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(url).Results;
        }

        public static List<Legislator> Locate(int zip)
        {
            string url = string.Format("{0}?zip={1}&apikey={2}", Settings.LegislatorsLocateUrl, zip, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(url).Results;
        }

        public static List<Legislator> Locate(double latitude, double longitude)
        {
            string url = string.Format("{0}?latitude={1}&longitude={2}&apikey={3}", Settings.LegislatorsLocateUrl, latitude, longitude, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(url).Results;
        }
        
        public static List<Legislator> Filter(Legislator.Filters filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.LegislatorsUrl, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }

    public class Term
    {
        [JsonProperty("start")]
        public DateTime? Start { get; set; }

        [JsonProperty("end")]
        public DateTime? End { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("party")]
        public string Party { get; set; }

        [JsonProperty("class")]
        public int? Class { get; set;}

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }
    }
}