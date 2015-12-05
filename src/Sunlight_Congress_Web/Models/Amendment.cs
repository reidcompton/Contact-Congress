using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
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

        [JsonProperty("introduced_on")]
        public DateTime? IntroducedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateTime? LastActionAt { get; set; }

        [JsonProperty("amends_amendment_id")]
        public string AmendsAmendmentId { get; set; }

        [JsonProperty("amends_amendment")]
        public Amendment AmendsAmendment { get; set; }

        [JsonProperty("amends_bill_id")]
        public string AmendsBillId { get; set; }

        [JsonProperty("amends_bill")]
        public Bill AmendsBill { get; set; }

        [JsonProperty("sponsor_type")]
        public string SponsorType { get; set; }

        [JsonProperty("sponsor_id")]
        public string SponsorId { get; set; }

        [JsonProperty("sponsor")]
        public Legislator Sponsor { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("actions")]
        public Action[] Actions { get; set; }
    }
}
