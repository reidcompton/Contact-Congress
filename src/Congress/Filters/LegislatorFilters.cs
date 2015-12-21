using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Congress.FilterBy
{ 
    public class Legislator : BasicRequest
    {
        [JsonProperty("birthday")]
        public DateFilter Birthday { get; set; }

        [JsonProperty("term_end")]
        public DateFilter TermEnd { get; set; }

        [JsonProperty("term_start")]
        public DateFilter TermStart { get; set; }

        [JsonProperty("aliases")]
        public StringFilter Aliases { get; set; }

        [JsonProperty("bioguide_id")]
        public StringFilter BioguideID { get; set; }

        [JsonProperty("campaign_twitter_ids")]
        public StringFilter CampaignTwitterIds { get; set; }

        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("contact_form")]
        public StringFilter ContactForm { get; set; }

        [JsonProperty("crp_id")]
        public StringFilter CrpId { get; set; }

        [JsonProperty("district")]
        public IntFilter District { get; set; }

        [JsonProperty("fec_ids")]
        public StringFilter FecIds { get; set; }

        [JsonProperty("first_name")]
        public StringFilter FirstName { get; set; }

        [JsonProperty("gender")]
        public StringFilter Gender { get; set; }

        [JsonProperty("govtrack_id")]
        public StringFilter GovTrackId { get; set; }

        [JsonProperty("icpsr_id")]
        public IntFilter IcpsrId { get; set; }

        [JsonProperty("in_office")]
        public bool? InOffice { get; set; }

        [JsonProperty("last_name")]
        public StringFilter LastName { get; set; }

        [JsonProperty("leadership_role")]
        public StringFilter LeadershipRole { get; set; }

        [JsonProperty("lis_id")]
        public StringFilter LisId { get; set; }

        [JsonProperty("middle_name")]
        public StringFilter MiddleName { get; set; }

        [JsonProperty("name_suffix")]
        public StringFilter NameSuffix { get; set; }

        [JsonProperty("nickname")]
        public StringFilter Nickname { get; set; }

        [JsonProperty("oc_email")]
        public StringFilter OcEmail { get; set; }

        [JsonProperty("ocd_id")]
        public StringFilter OcdId { get; set; }

        [JsonProperty("party")]
        public StringFilter Party { get; set; }

        [JsonProperty("state")]
        public StringFilter State { get; set; }

        [JsonProperty("state_name")]
        public StringFilter StateName { get; set; }

        [JsonProperty("thomas_id")]
        public StringFilter ThomasId { get; set; }

        [JsonProperty("title")]
        public StringFilter Title { get; set; }

        [JsonProperty("twitter_id")]
        public StringFilter TwitterId { get; set; }

        [JsonProperty("votesmart_id")]
        public IntFilter VoteSmartId { get; set; }
    }
}
