using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Congress
{
    public class Client
    {
        private static string _apiKey { get; set; }
        public Client(string apiKey)
        {
            _apiKey = apiKey;
        }
        
        public Amendment[] Amendments()
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, _apiKey);
            return Helpers.Get<AmendmentWrapper>(url).Results.ToArray();
        }

        public Amendment[] Amendments(FilterBy.Amendment filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, Settings.Token);
            return Helpers.Get<AmendmentWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public Bill[] Bills()
        {
            string url = string.Format("{0}?apikey={1}", Settings.BillsUrl, Settings.Token);
            return Helpers.Get<BillWrapper>(url).Results.ToArray();
        }
        
        public static Bill[] Bills(FilterBy.Bill filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.BillsUrl, Settings.Token);
            return Helpers.Get<BillWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public static Bill[] Bills(string query, FilterBy.Bill filters = null)
        {
            string url = string.Format("{0}?apikey={1}&query={2}", Settings.BillsSearchUrl, Settings.Token, query);
            if (filters != null)
                url = Helpers.QueryString(url, filters);
            return Helpers.Get<BillWrapper>(url).Results.ToArray();
        }

        public static Committee[] Committees()
        {
            string url = string.Format("{0}?apikey={1}", Settings.CommitteesUrl, Settings.Token);
            return Helpers.Get<CommitteeWrapper>(url).Results.ToArray();
        }

        public static Committee[] Committees(FilterBy.Committee filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.CommitteesUrl, Settings.Token);
            return Helpers.Get<CommitteeWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public static CongressionalDocument[] CongressionalDocuments() 
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<CongressionalDocumentWrapper>(url).Results.ToArray();
        }

        public static District[] Districts(int zip)
        {
            string url = string.Format("{0}?zip={1}&apikey={2}", Settings.DistrictsLocateUrl, zip, Settings.Token);
            return Helpers.Get<DistrictWrapper>(url).Results.ToArray();
        }

        public static District[] Districts(double latitude, double longitude)
        {
            string url = string.Format("{0}?latitude={1}&longitude={2}&apikey={3}", Settings.DistrictsLocateUrl, latitude, longitude, Settings.Token);
            return Helpers.Get<DistrictWrapper>(url).Results.ToArray();
        }

        public static Document[] Documents()
        {
            string url = string.Format("{0}?apikey={1}", Settings.DocumentsSearchUrl, Settings.Token);
            return Helpers.Get<DocumentWrapper>(url).Results.ToArray();
        }

        public static FloorUpdate[] FloorUpdates()
        {
            string url = string.Format("{0}?apikey={1}", Settings.FloorUpdatesUrl, Settings.Token);
            return Helpers.Get<FloorUpdateWrapper>(url).Results.ToArray();
        }

        public static FloorUpdate[] FloorUpdates(FilterBy.FloorUpdate filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.FloorUpdatesUrl, Settings.Token);
            return Helpers.Get<FloorUpdateWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public static Hearing[] Hearings()
        {
            string url = string.Format("{0}?apikey={1}", Settings.HearingsUrl, Settings.Token);
            return Helpers.Get<HearingWrapper>(url).Results.ToArray();
        }

        public static Hearing[] Hearings(FilterBy.Hearing filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.HearingsUrl, Settings.Token);
            return Helpers.Get<HearingWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public static Legislator[] Legislators()
        {
            string url = string.Format("{0}?apikey={1}", Settings.LegislatorsUrl, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(url).Results.ToArray();
        }

        public static Legislator[] Legislators(int zip)
        {
            string url = string.Format("{0}?zip={1}&apikey={2}", Settings.LegislatorsLocateUrl, zip, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(url).Results.ToArray();
        }

        public static Legislator[] Legislators(double latitude, double longitude)
        {
            string url = string.Format("{0}?latitude={1}&longitude={2}&apikey={3}", Settings.LegislatorsLocateUrl, latitude, longitude, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(url).Results.ToArray();
        }

        public static Legislator[] Legislators(FilterBy.Legislator filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.LegislatorsUrl, Settings.Token);
            return Helpers.Get<LegislatorWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public static Nomination[] Nominations()
        {
            string url = string.Format("{0}?apikey={1}", Settings.AmendmentsUrl, Settings.Token);
            return Helpers.Get<NominationWrapper>(url).Results.ToArray();
        }

        public static Nomination[] Nomination(FilterBy.Nomination filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.NominationsUrl, Settings.Token);
            return Helpers.Get<NominationWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public static UpcomingBill[] UpcomingBills()
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<UpcomingBillWrapper>(url).Results.ToArray();
        }

        public static UpcomingBill[] UpcomingBills(FilterBy.UpcomingBill filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.UpcomingBillsUrl, Settings.Token);
            return Helpers.Get<UpcomingBillWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }

        public static Vote[] Votes()
        {
            string url = string.Format("{0}?apikey={1}", Settings.VotesUrl, Settings.Token);
            return Helpers.Get<VoteWrapper>(url).Results.ToArray();
        }

        public static Vote[] Votes(FilterBy.Vote filters)
        {
            string url = string.Format("{0}?apikey={1}", Settings.VotesUrl, Settings.Token);
            return Helpers.Get<VoteWrapper>(Helpers.QueryString(url, filters)).Results.ToArray();
        }
    }
}
