using Newtonsoft.Json;

namespace Congress.FilterBy
{
    public class Bill : BasicRequest
    {
        [JsonProperty("bill_id")]
        public StringFilter BillId { get; set; }

        [JsonProperty("bill_type")]
        public StringFilter BillType { get; set; }

        [JsonProperty("number")]
        public IntFilter Number { get; set; }

        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("chamber")]
        public StringFilter Chamber { get; set; }

        [JsonProperty("introduced_on")]
        public DateFilter IntroducedOn { get; set; }

        [JsonProperty("last_action_at")]
        public DateFilter LastActionAt { get; set; }

        [JsonProperty("last_vote_at")]
        public DateFilter LastVoteAt { get; set; }

        [JsonProperty("last_version_on")]
        public DateFilter LastVersionOn { get; set; }

        [JsonProperty("nicknames")]
        public StringFilter Nicknames { get; set; }

        [JsonProperty("keywords")]
        public StringFilter Keywords { get; set; }

        [JsonProperty("sponsor_id")]
        public StringFilter SponsorId { get; set; }

        [JsonProperty("cosponsor_ids")]
        public StringFilter CoSponsorIds { get; set; }

        [JsonProperty("cosponsors_count")]
        public IntFilter CoSponsorsCount { get; set; }

        [JsonProperty("withdrawn_cosponsor_ids")]
        public StringFilter WithdrawnCoSponsorIds { get; set; }

        [JsonProperty("withdrawn_cosponsors_count")]
        public IntFilter WithdrawnCoSponsorsCount { get; set; }

        [JsonProperty("committee_ids")]
        public StringFilter CommitteeIds { get; set; }

        [JsonProperty("related_bill_ids")]
        public StringFilter RelatedBillIds { get; set; }

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
        public DateFilter ActiveAt { get; set; }

        [JsonProperty("house_passage_result")]
        public StringFilter HousePassageResult { get; set; }

        [JsonProperty("house_passage_result_at")]
        public DateFilter HousePassageResultAt { get; set; }

        [JsonProperty("senate_cloture_result")]
        public StringFilter SenateClotureResult { get; set; }

        [JsonProperty("senate_cloture_result_at")]
        public DateFilter SenateClotureResultAt { get; set; }

        [JsonProperty("senate_passage_result")]
        public StringFilter SenatePassageResult { get; set; }

        [JsonProperty("senate_passage_result_at")]
        public DateFilter SenatePassageResultAt { get; set; }

        [JsonProperty("vetoed")]
        public bool? Vetoed { get; set; }

        [JsonProperty("awaiting_signature")]
        public bool? AwaitingSignature { get; set; }

        [JsonProperty("enacted")]
        public bool? Enacted { get; set; }

        [JsonProperty("enacted_at")]
        public DateFilter EnactedAt { get; set; }
    }

    public class EnactedAs
    {
        [JsonProperty("congress")]
        public IntFilter Congress { get; set; }

        [JsonProperty("law_type")]
        public StringFilter LawType { get; set; }

        [JsonProperty("number")]
        public IntFilter Number { get; set; }
    }
}
