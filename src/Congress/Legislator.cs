using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Congress
{
    public class Legislators : SunlightData<Legislator>
    {
        public Legislators(string apiKey) : base(apiKey)
        {
            _apiKey = apiKey;
        }
        public Legislators(string apiKey, Expression expression) : base(apiKey, expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }
    }

    public class Legislator : BasicRequest
    {
        // queryable fields
        [JsonProperty("zip")]
        public int? Zip { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonProperty("term_end")]
        public DateTime? TermEnd { get; set; }

        [JsonProperty("term_start")]
        public DateTime? TermStart { get; set; }

        [JsonProperty("aliases")]
        public string Aliases { get; set; }

        [JsonProperty("bioguide_id")]
        public string BioguideID { get; set; }

        [JsonProperty("campaign_twitter_ids")]
        public string CampaignTwitterIds { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("contact_form")]
        public string ContactForm { get; set; }

        [JsonProperty("crp_id")]
        public string CrpId { get; set; }

        [JsonProperty("district")]
        public int? District { get; set; }

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
        public bool? InOffice { get; set; }

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

        [JsonProperty("party")]
        public string Party { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state_name")]
        public string StateName { get; set; }

        [JsonProperty("thomas_id")]
        public string ThomasId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("twitter_id")]
        public string TwitterId { get; set; }

        [JsonProperty("votesmart_id")]
        public int? VoteSmartId { get; set; }

        // non-queryable fields
        [JsonProperty("facebook_id")]
        private string FacebookId { get; set; }

        [JsonProperty("fax")]
        private string Fax { get; set; }

        [JsonProperty("office")]
        private string Office { get; set; }

        [JsonProperty("phone")]
        private string Phone { get; set; }

        [JsonProperty("terms")]
        private Term[] Terms { get; set; }

        [JsonProperty("website")]
        private string Website { get; set; }

        [JsonProperty("youtube_id")]
        private string YouTubeId { get; set; }
    }

    public class Term
    {
        [JsonProperty("start")]
        private DateTime? Start { get; set; }

        [JsonProperty("end")]
        private DateTime? End { get; set; }

        [JsonProperty("state")]
        private string State { get; set; }

        [JsonProperty("party")]
        private string Party { get; set; }

        [JsonProperty("class")]
        private int? Class { get; set;}

        [JsonProperty("title")]
        private string Title { get; set; }

        [JsonProperty("chamber")]
        private string Chamber { get; set; }
    }
}