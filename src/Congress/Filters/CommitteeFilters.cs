﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sunlight_Congress
{ 
    public class CommitteeFilters
    {
        [JsonProperty("committee_id")]
        public string CommitteeId { get; set; }

        [JsonProperty("chamber")]
        public string Chamber { get; set; }

        [JsonProperty("subcommittee")]
        public bool? SubCommittee { get; set; }

        [JsonProperty("member_ids")]
        public string[] MemberIds { get; set; }

        [JsonProperty("parent_committee_id")]
        public string ParentCommitteeId { get; set; }
    }
}