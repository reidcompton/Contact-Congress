using System;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Sunlight_Congress_Web.Models;

namespace Sunlight_Congress
{

    public class LegislatorWrapper
    {
        [JsonProperty("results")]
        public List<Legislator> Results { get; set; }
    }

    public class Legislator
    {
        [JsonProperty("aliases")]
        public string[] Aliases { get; set; }

        [JsonProperty("bioguide_id")]
        public string BioguideID { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonProperty("campaign_twitter_ids")]
        public string[] CampaignTwitterIds { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("contact_form")]
        public string ContactForm { get; set; }

        [JsonProperty("crp_id")]
        public string CrpId { get; set; }

        [JsonProperty("district")]
        public int? District { get; set; }

        [JsonProperty("facebook_id")]
        public string FacebookId { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("fec_ids")]
        public string[] FecIds { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("govtrack_id")]
        public string GovTrackId { get; set; }

        [JsonProperty("icpsr_id")]
        public int? IcpsrId { get; set; }

        [JsonProperty("in_office")]
        public string InOffice { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("leadership_role")]
        public string LeadershipRole { get; set; }

        [JsonProperty("lis_id")]
        public string LisId { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("name_suffix")]
        public string NameSuffix { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("oc_email")]
        public string OcEmail { get; set; }

        [JsonProperty("ocd_id")]
        public string OcdId { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("party")]
        public string Party { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state_name")]
        public string StateName { get; set; }

        [JsonProperty("terms")]
        public Term[] Terms { get; set; }

        [JsonProperty("term_end")]
        public DateTime? TermEnd { get; set; }

        [JsonProperty("term_start")]
        public DateTime? TermStart { get; set; }

        [JsonProperty("thomas_id")]
        public string ThomasId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("twitter_id")]
        public string TwitterId { get; set; }

        [JsonProperty("votesmart_id")]
        public int? VoteSmartId { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("youtube_id")]
        public string YouTubeId { get; set; }


        

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
        
        public static List<Legislator> Filter(Legislator filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.LegislatorsUrl, Settings.Token);
            var props = filters.GetType().GetProperties();
            for (int i = 0; i< props.Length; i++)
            {
                JsonPropertyAttribute key = filters.GetType().GetProperty(props[i].Name).GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0] as JsonPropertyAttribute;
                var value = filters.GetType().GetProperty(props[i].Name).GetValue(filters, null);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    url += string.Format("&{0}={1}", key.PropertyName, Helpers.ConvertToSafeString(value));
            }
            return Helpers.Get<LegislatorWrapper>(url).Results;
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