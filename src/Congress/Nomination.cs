﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Congress
{ 
    public class NominationWrapper : BasicReponse
    {
        [JsonProperty("results")]
        public List<Nomination> Results { get; set; }
    }

    public class Nomination : NominationFilters
    {
        [JsonProperty("actions")]
        public NominationAction[] Actions { get; set; }

        [JsonProperty("last_action")]
        public NominationAction LastAction { get; set; }

        public class Filters : NominationFilters { }

        public static List<Nomination> All()
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, Settings.Token);
            return Helpers.Get<NominationWrapper>(url).Results;
        }

        public static List<Nomination> Filter(Nomination.Filters filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.NominationsUrl, Settings.Token);
            return Helpers.Get<NominationWrapper>(Helpers.QueryString(url, filters)).Results;
        }
    }

    public class NominationAction
    {
        [JsonProperty("acted_at")]
        public DateTime? ActedAt { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Nominee
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
