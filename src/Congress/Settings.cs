namespace Congress
{
    internal class Settings
    {
        public static string BaseUrl
        {
            get { return "https://congress.api.sunlightfoundation.com"; }
        }

        public static string LegislatorsUrl
        {
            get { return string.Concat(BaseUrl, "/legislators"); }
        }

        public static string LegislatorsLocateUrl
        {
            get { return string.Concat(BaseUrl, "/legislators/locate"); }
        }

        public static string DistrictsLocateUrl
        {
            get { return string.Concat(BaseUrl, "/districts/locate"); }
        }

        public static string CommitteesUrl
        {
            get { return string.Concat(BaseUrl, "/committees"); }
        }

        public static string BillsUrl
        {
            get { return string.Concat(BaseUrl, "/bills"); }
        }

        public static string BillsSearchUrl
        {
            get { return string.Concat(BaseUrl, "/bills/search"); }
        }

        public static string AmendmentsUrl
        {
            get { return string.Concat(BaseUrl, "/amendments"); }
        }

        public static string NominationsUrl
        {
            get { return string.Concat(BaseUrl, "/nominations"); }
        }

        public static string VotesUrl
        {
            get { return string.Concat(BaseUrl, "/votes"); }
        }

        public static string FloorUpdatesUrl
        {
            get { return string.Concat(BaseUrl, "/floor_updates"); }
        }

        public static string HearingsUrl
        {
            get { return string.Concat(BaseUrl, "/hearings"); }
        }

        public static string UpcomingBillsUrl
        {
            get { return string.Concat(BaseUrl, "/upcoming_bills"); }
        }

        public static string CongressionalDocumentsSearchUrl
        {
            get { return string.Concat(BaseUrl, "/congressional_documents/search"); }
        }

        public static string DocumentsSearchUrl
        {
            get { return string.Concat(BaseUrl, "/documents/search"); }
        }

    }
}
