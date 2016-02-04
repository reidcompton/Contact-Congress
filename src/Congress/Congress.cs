using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Congress
{
    public class Congress
    {
        private static string _apiKey { get; set; }
        public Congress(string apiKey)
        {
            _apiKey = apiKey;
            this.Amendments = new Amendments(apiKey);
            this.Bills = new Bills(apiKey);
            this.Committees = new Committees(apiKey);
            this.CongressionalDocuments = new CongressionalDocuments(apiKey);
            this.Districts = new Districts(apiKey);
            this.Documents = new Documents(apiKey);
            this.FloorUpdates = new FloorUpdates(apiKey);
            this.Hearings = new Hearings(apiKey);
            this.Legislators = new Legislators(apiKey);
            this.Nominations = new Nominations(apiKey);
            this.UpcomingBills = new UpcomingBills(apiKey);
            this.Votes = new Votes(apiKey);
        }

        public Amendments Amendments { get; }
        public Bills Bills{ get; set; }
        public Committees Committees { get; set; }
        public CongressionalDocuments CongressionalDocuments { get; set; }
        public Districts Districts { get; set; }
        public Documents Documents { get; set; }
        public FloorUpdates FloorUpdates { get; set; }
        public Hearings Hearings { get; set; }
        public Legislators Legislators { get; set; }
        public Nominations Nominations { get; set; }
        public UpcomingBills UpcomingBills { get; set; }
        public Votes Votes { get; set; }
    }
}
