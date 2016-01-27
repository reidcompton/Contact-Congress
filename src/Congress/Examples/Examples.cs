using System;
using System.Linq;
using System.Linq.Expressions;

namespace Congress
{
    public class Tests
    {
        public static string RunTests()
        {

            Client client = new Client("d50b80bc9cfe43be821059b6470e4ab9");

            Amendments amendments = new Amendments("d50b80bc9cfe43be821059b6470e4ab9");
            
            //AmendmentFilter[] s = amendments.Where(x => x.AmendmentId == "samdt2952-114").ToArray();

            //AmendmentFilter[] s1 = client.Amendments.Where(x =>x.AmendmentId == "samdt2952-114").ToArray();


            // Amendment All
            Amendment[] a = Amendment.All().ToArray();

            // Amendment Filter
            AmendmentFilter[] b = amendments.Where(x =>
                                x.PerPage == 3 &&
                                x.AmendmentId == "samdt2921-114" &&
                                x.Congress == 114 &&
                                x.Number == 2921 &&
                                x.Chamber == "senate" &&
                                x.AmendmentType == "samdt" &&
                                x.IntroducedOn > new DateTime(2015, 12, 7) &&
                                x.SponsorType == "person" &&
                                x.SponsorId == "C001070" &&
                                x.AmendsBillId == "sres207-114"
                            ).ToArray();

            Bills bills = new Bills("d50b80bc9cfe43be821059b6470e4ab9");

            //// Bill All
            Bill[] c = Bill.All().ToArray();

            //// Bill Filter
            BillFilter[] d = bills.Where(x =>
                        x.BillId == "hr4193-114" &&
                        x.BillType == "hr" &&
                        x.Chamber == "house" &&
                        //x.CommitteeIds == "HSII" &&
                        x.Congress == 114 &&
                        x.CoSponsorsCount == 0 &&
                        x.History.Active == false &&
                        x.History.AwaitingSignature == false &&
                        x.History.Enacted == false &&
                        x.History.Vetoed == false &&
                        x.IntroducedOn == new DateTime(2015, 12, 8) &&
                        x.Number == 4193 &&
                        x.SponsorId == "Y000033"
                    ).ToArray();

            // TODO allow for sub-objects
            //History[] d1 = bills.Where(x => x.SponsorId == "Y000033").Select(x => x.History).ToArray();

            //// Bill Search
            BillFilter[] e = bills.Where(x => x.Query == "To authorize the expansion of an existing hydroelectric project.").ToArray();

            //// Committee All
            Committee[] f = Committee.All().ToArray();

            Committees committees = new Committees("d50b80bc9cfe43be821059b6470e4ab9");

            //// Committee Filter
            Committee[] g = committees.Where(x =>
                x.Chamber == "senate" &&
                x.CommitteeId == "SSGA19" &&
                x.ParentCommitteeId == "SSGA" &&
                x.SubCommittee == true
            ).ToArray();

            //// Congressional Document All
            CongressionalDocument[] h = CongressionalDocument.All().ToArray();
            

            //// District Locate by Zip
            //District[] i = District.Search(60657).ToArray();

            //// District Locate By Lat/Long
            //District[] j = District.Search(42.96, -108.09).ToArray();

            //// Document All
            Document[] k = Document.All().ToArray();

            //// Floor Update All
            FloorUpdate[] l = FloorUpdate.All().ToArray();

            FloorUpdates floorUpdates = new FloorUpdates("d50b80bc9cfe43be821059b6470e4ab9");

            //// Floor Update Filter
            FloorUpdate[] m = floorUpdates.Where(x =>
                x.Chamber == "senate" &&
                x.Congress == 114 &&
                x.LegislativeDay == new DateTime(2015, 12, 9)
            ).ToArray();

            //// Hearing All
            Hearing[] n = Hearing.All().ToArray();

            Hearings hearings = new Hearings("d50b80bc9cfe43be821059b6470e4ab9");

            //// Hearing Filter 
            Hearing[] o = hearings.Where(x =>
                x.CommitteeId == "HSSM" && 
                x.Chamber == "house" &&
                x.Dc == true &&
                x.Congress == 114 &&
                x.HearingType == "Hearing"
            ).ToArray();

            //// Legislator All
            Legislator[] p = Legislator.All().ToArray();

            //// Legislator Locate by Zip
            //Legislator[] q = Legislator.Search(60657).ToArray();

            //// Legislator Locate by Lat/Long
            //Legislator[] r = Legislator.Search(42.96, -108.09).ToArray();

            Legislators legislators = new Legislators("d50b80bc9cfe43be821059b6470e4ab9");

            //// Legislator Filter
            string[] s = legislators.Where(x =>
                x.BioguideID == "L000585" &&
                x.Birthday > new DateTime(1968, 7, 4) &&
                x.Chamber == "house" &&
                x.CrpId == "N00037031" &&
                x.District == 18 &&
                //x.FecIds == new string[] { "H6IL18088" } &&
                x.FirstName == "Darin" &&
                x.Gender == "M" &&
                x.GovTrackId == "412674" &&
                x.InOffice == true &&
                x.LastName == "LaHood" &&
                x.Party == "R" &&
                x.State == "IL" &&
                x.VoteSmartId == 128760
            ).Select(x => x.GovTrackId).ToArray();

            //Bill[] bills = Bill.Search(new FilterBy.Bill() {
            //    SponsorId = new StringFilter(s[0].BioguideID)
            //}).ToArray();

            //// Nomination All
            Nomination[] t = Nomination.All().ToArray();

            Nominations nominations = new Nominations("d50b80bc9cfe43be821059b6470e4ab9");

            // Nomination Filter
            Nomination[] u = nominations.Where(xy =>
                xy.NominationId == "PN951-02-114" &&
                xy.Congress == 114 &&
                xy.Number == "951-02" &&
                xy.Organization == "Foreign Service" &&
                //xy.CommitteeIds == "SSFR" &&
                xy.LastActionAt == new DateTime(2015, 11, 19)
            ).ToArray();

            //// Upcoming Bill All
            UpcomingBill[] v = UpcomingBill.All().ToArray();

            UpcomingBills upcomingBills = new UpcomingBills("d50b80bc9cfe43be821059b6470e4ab9");

            //// Upcoming Bill Filter
            UpcomingBill[] w = upcomingBills.Where(xy =>
                xy.BillId == "s1177-114" &&
                xy.Chamber == "senate" &&
                xy.Congress == 114 &&
                xy.LegislativeDay == new DateTime(2015, 12, 9) &&
                xy.Range == "day" &&
                xy.SourceType == "senate_daily"
            ).ToArray();

            //// Vote All
            Vote[] xyz = Vote.All().ToArray();

            Votes votes = new Votes("d50b80bc9cfe43be821059b6470e4ab9");

            //// Vote Filter
            Vote[] y = votes.Where(xy =>
                        xy.BillId == "hr2130-114" &&
                        xy.Chamber == "house" &&
                        xy.Congress == 114 &&
                        xy.Number == 684 &&
                        xy.Required == "1/2" &&
                        xy.RollId == "h684-2015" &&
                        xy.VoteType == "amendment"
                    ).ToArray();

            // Vote Filter by Breakdown
            Vote[] z = votes.Where(xy => xy.Breakdown.Party.Republican.Yea > 30).ToArray();

            string result = string.Format(
                "a: {0}</p><p> b: {1}</p><p> c: {2}</p><p> d: {3}</p><p> e: {4}</p><p> f: {5}</p><p> g: {6}</p><p> h: {7}</p><p> i: {8}</p><p> j: {9}</p><p> k: {10}</p><p> l: {11}</p><p> m: {12}</p><p> n: {13}</p><p> o: {14}</p><p> p: {15}</p><p> q: {16}</p><p> r: {17}</p><p> s: {18}</p><p> t: {19}</p><p> u: {20}</p><p> v: {21}</p><p> w: {22}</p><p> x: {23}</p><p> y: {24} </p><p> z: {25}",
                a.Length > 0,
                b.Length > 0,
                c.Length > 0,
                d.Length > 0,
                e.Length > 0,
                f.Length > 0,
                g.Length > 0,
                h.Length > 0,
                false,//i.Length > 0,
                false,//j.Length > 0,
                k.Length > 0,
                l.Length > 0,
                m.Length > 0,
                n.Length > 0,
                o.Length > 0,
                p.Length > 0,
                false,//q.Length > 0,
                false,//r.Length > 0,
                s,//s.Length > 0,
                t.Length > 0,
                u.Length > 0,
                v.Length > 0,
                w.Length > 0,
                xyz.Length > 0,
                y.Length > 0,
                z.Length > 0
            );
            return result;
        }
    }
}
