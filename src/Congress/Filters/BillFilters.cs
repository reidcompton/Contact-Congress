using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{
    public class BillFilters
    {
        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("bill_type")]
        public string BillType { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("introduced_on")]
        public DateTime? IntroducedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateTime? LastActionAt { get; set; }

        [JsonProperty("last_vote_at")]
        public DateTime? LastVoteAt { get; set; }

        [JsonProperty("last_version_on")]
        public DateTime? LastVersionOn { get; set; }

        [JsonProperty("nicknames")]
        public string[] Nicknames { get; set; }

        [JsonProperty("keywords")]
        public string[] Keywords { get; set; }

        [JsonProperty("sponsor_id")]
        public string SponsorId { get; set; }

        [JsonProperty("cosponsor_ids")]
        public string[] CoSponsorIds { get; set; }

        [JsonProperty("cosponsors_count")]
        public int? CoSponsorsCount { get; set; }

        [JsonProperty("withdrawn_cosponsor_ids")]
        public string[] WithdrawnCoSponsorIds { get; set; }

        [JsonProperty("withdrawn_cosponsors_count")]
        public int? WithdrawnCoSponsorsCount { get; set; }

        [JsonProperty("committee_ids")]
        public string[] CommitteeIds { get; set; }

        [JsonProperty("related_bill_ids")]
        public string[] RelatedBillIds { get; set; }

        [JsonProperty("history")]
        public History History { get; set; }

        [JsonProperty("enacted_as")]
        public EnactedAs EnactedAs { get; set; }
    }

    public class History
    {
        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("active_at")]
        public DateTime? ActiveAt { get; set; }

        [JsonProperty("house_passage_result")]
        public string HousePassageResult { get; set; }

        [JsonProperty("house_passage_result_at")]
        public DateTime? HousePassageResultAt { get; set; }

        [JsonProperty("senate_cloture_result")]
        public string SenateClotureResult { get; set; }

        [JsonProperty("senate_cloture_result_at")]
        public DateTime? SenateClotureResultAt { get; set; }

        [JsonProperty("senate_passage_result")]
        public string SenatePassageResult { get; set; }

        [JsonProperty("senate_passage_result_at")]
        public DateTime? SenatePassageResultAt { get; set; }

        [JsonProperty("vetoed")]
        public bool? Vetoed { get; set; }

        [JsonProperty("awaiting_signature")]
        public bool? AwaitingSignature { get; set; }

        [JsonProperty("enacted")]
        public bool? Enacted { get; set; }

        [JsonProperty("enacted_at")]
        public DateTime? EnactedAt { get; set; }
    }

    public class EnactedAs
    {
        [JsonProperty("congress")]
        public int? Congress { get; set; }

        [JsonProperty("law_type")]
        public string LawType { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }
    }
}
