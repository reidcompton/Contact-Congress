using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Congress
{
    public class AmendmentWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<Amendment> Results { get; set; }
    }
    
    public class Amendment
    {
        [JsonProperty("amendment_id")]
        public string AmendmentId { get; set; }

        [JsonProperty("amendment_type")]
        public string AmendmentType { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("house_number")]
        public int? HouseNumber { get; set; }

        [JsonProperty("amends_amendment_id")]
        public string AmendsAmendmentId { get; set; }

        [JsonProperty("amends_bill_id")]
        public string AmendsBillId { get; set; }

        [JsonProperty("sponsor_type")]
        public string SponsorType { get; set; }

        [JsonProperty("sponsor_id")]
        public string SponsorId { get; set; }

        [JsonProperty("introduced_on")]
        public DateTime? IntroducedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateTime? LastActionAt { get; set; }

        [JsonProperty("amends_amendment")]
        public Amendment AmendsAmendment { get; set; }

        [JsonProperty("amends_bill")]
        public Bill AmendsBill { get; set; }

        [JsonProperty("sponsor")]
        public Legislator Sponsor { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("actions")]
        public Action[] Actions { get; set; }

        public static List<Amendment> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, Settings.Token);
            return Helpers.Get<AmendmentWrapper>(url).Results;
        }

        public static List<Amendment> Search(FilterBy.Amendment filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, Settings.Token);
            return Helpers.Get<AmendmentWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }
}
