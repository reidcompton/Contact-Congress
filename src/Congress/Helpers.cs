using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Congress
{
    public class Helpers
    {
        public static string RunTests()
        {
            // Amendment All
            Amendment[] a = Amendment.All().ToArray();

            // Amendment Filter
            Amendment[] b = Amendment.Filter(new Amendment.Filters() {
                AmendmentId = "samdt2921-114",
                Congress = 114,
                Number = 2921,
                Chamber = "senate",
                AmendmentType = "samdt",
                IntroducedOn = new DateTime(2015, 12, 8),
                SponsorType = "person",
                SponsorId = "C001070",
                AmendsBillId = "sres207-114"
            }).ToArray();

            // Bill All
            Bill[] c = Bill.All().ToArray();

            // Bill Filter
            Bill[] d = Bill.Filter(new Bill.Filters() {
                BillId = "hr4193-114",
                BillType = "hr",
                Chamber = "house",
                CommitteeIds = new string[] { "HSII" },
                Congress = 114,
                CoSponsorsCount = 0,
                History = new History() {
                    Active = false,
                    AwaitingSignature = false,
                    Enacted = false,
                    Vetoed = false
                },
                IntroducedOn = new DateTime(2015,12,8),
                Number = 4193,
                SponsorId = "Y000033"
            }).ToArray();

            // Bill Search
            Bill[] e = Bill.Search("To authorize the expansion of an existing hydroelectric project.").ToArray();

            // Committee All
            Committee[] f = Committee.All().ToArray();

            // Committee Filter
            Committee[] g = Committee.Filter(new Committee.Filters() {
                Chamber = "senate",
                CommitteeId = "SSGA19",
                ParentCommitteeId = "SSGA",
                SubCommittee = true
            }).ToArray();

            // Congressional Document All
            CongressionalDocument[] h = CongressionalDocument.All().ToArray();

            // District Locate by Zip
            District[] i = District.Locate(60657).ToArray();

            // District Locate By Lat/Long
            District[] j = District.Locate(42.96, -108.09).ToArray();

            // Document All
            Document[] k = Document.All().ToArray();

            // Floor Update All
            FloorUpdate[] l = FloorUpdate.All().ToArray();

            // Floor Update Filter
            FloorUpdate[] m = FloorUpdate.Filter(new FloorUpdate.Filters() {
                Chamber = "senate",
                Congress = 114,
                LegislativeDay = new DateTime(2015, 12, 9)
            }).ToArray();

            // Hearing All
            Hearing[] n = Hearing.All().ToArray();

            // Hearing Filter 
            Hearing[] o = Hearing.Filter(new Hearing.Filters() {
                CommitteeId = "HSSM",
                Chamber = "house",
                Dc = true,
                Congress = 114,
                HearingType = "Hearing"
            }).ToArray();

            // Legislator All
            Legislator[] p = Legislator.All().ToArray();

            // Legislator Locate by Zip
            Legislator[] q = Legislator.Locate(60657).ToArray();

            // Legislator Locate by Lat/Long
            Legislator[] r = Legislator.Locate(42.96, -108.09).ToArray();

            // Legislator Filter
            Legislator[] s = Legislator.Filter(new Legislator.Filters() {
                BioguideID = "L000585",
                Birthday = new DateTime(1968, 7, 5),
                Chamber = "house",
                CrpId = "N00037031",
                District = 18,
                FecIds = new string[] { "H6IL18088" },
                FirstName = "Darin",
                Gender = "M",
                GovTrackId = "412674",
                InOffice = true,
                LastName = "LaHood",
                Party = "R",
                State = "IL",
                VoteSmartId = 128760
            }).ToArray();

            // Nomination All
            Nomination[] t = Nomination.All().ToArray();

            // Nomination Filter
            Nomination[] u = Nomination.Filter(new Nomination.Filters() {
                NominationId = "PN951-02-114",
                Congress = 114,
                Number = "951-02",
                Organization = "Foreign Service",
                CommitteeIds = new string[] { "SSFR" },
                LastActionAt = new DateTime(2015, 11, 19)
            }).ToArray();

            // Upcoming Bill All
            UpcomingBill[] v = UpcomingBill.All().ToArray();

            // Upcoming Bill Filter
            UpcomingBill[] w = UpcomingBill.Filter(new UpcomingBill.Filters() {
                BillId = "s1177-114",
                Chamber = "senate",
                Congress = 114,
                LegislativeDay = new DateTime(2015, 12, 9),
                Range = "day",
                SourceType = "senate_daily"
            }).ToArray();

            // Vote All
            Vote[] x = Vote.All().ToArray();

            // Vote Filter
            Vote[] y = Vote.Filter(new Vote.Filters() {
                BillId = "hr2130-114",
                Chamber = "house",
                Congress = 114,
                Number = 684,
                Required = "1/2",
                RollId = "h684-2015",
                VoteType = "amendment"
            }).ToArray();

            string result = string.Format(
                "a: {0}\n b: {1}\n c: {2}\n d: {3}\n e: {4}\n f: {5}\n g: {6}\n h: {7}\n i: {8}\n j: {9}\n k: {10}\n l: {11}\n m: {12}\n n: {13}\n o: {14}\n p: {15}\n q: {16}\n r: {17}\n s: {18}\n t: {19}\n u: {20}\n v: {21}\n w: {22}\n x: {23}\n y: {24}\n",
                a.Length > 0,
                b.Length > 0,
                c.Length > 0,
                d.Length > 0,
                e.Length > 0,
                f.Length > 0,
                g.Length > 0,
                h.Length > 0,
                i.Length > 0,
                j.Length > 0,
                k.Length > 0,
                l.Length > 0,
                m.Length > 0,
                n.Length > 0,
                o.Length > 0,
                p.Length > 0,
                q.Length > 0,
                r.Length > 0,
                s.Length > 0,
                t.Length > 0,
                u.Length > 0,
                v.Length > 0,
                w.Length > 0,
                x.Length > 0,
                y.Length > 0
            );
            return result;
        }

        public static string ConvertToSafeString<T>(T prop)
        {
            if (prop.GetType() == typeof(DateTime))
                return ((DateTime)(object)prop).ToString("yyyy-MM-dd");
            else if (prop.GetType() == typeof(bool))
                return prop.ToString().ToLower();
            else if (prop.GetType() == typeof(string[]))
                return ((string[])(object)prop)[0].ToString();
            else if (prop.GetType() == typeof(int))
                return ((int)(object)prop).ToString();
            else
                return string.Format("%22{0}%22",prop.ToString());
        }

        public static T Get<T>(string url)
        {
            using (System.Net.WebClient client = new WebClient())
            {
                client.BaseAddress = url;
                string response = client.DownloadString(client.BaseAddress);
                return JsonConvert.DeserializeObject<T>(response);
            }
        }

        public static string QueryString<T>(string url, T filters)
        {
            var props = filters.GetType().GetProperties();
            for (int i = 0; i<props.Length; i++)
            {
                JsonPropertyAttribute key = filters.GetType().GetProperty(props[i].Name).GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0] as JsonPropertyAttribute;
                var value = filters.GetType().GetProperty(props[i].Name).GetValue(filters, null);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    if(IsCustomClass(value))
                        url = ExtractPropertiesOnObjects(key.PropertyName, value, url);
                    else
                        url += string.Format("&{0}={1}", key.PropertyName, Helpers.ConvertToSafeString(value));
                }   
            }
            return url;
        }

        public static string ExtractPropertiesOnObjects<T>(string originalKey, T value, string url)
        {
            var props = value.GetType().GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                JsonPropertyAttribute key = value.GetType().GetProperty(props[i].Name).GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0] as JsonPropertyAttribute;
                var subValue = value.GetType().GetProperty(props[i].Name).GetValue(value, null);
                if (subValue != null && !string.IsNullOrEmpty(subValue.ToString()))
                {
                    url += string.Format("&{0}.{1}={2}", originalKey, key.PropertyName, Helpers.ConvertToSafeString(subValue));
                }
            }
            return url;
        }

        private static List<Type> _systemTypes;
        public static bool IsCustomClass<T>(T item)
        {
            if (_systemTypes == null)
                _systemTypes = Assembly.GetExecutingAssembly().GetType().Module.Assembly.GetExportedTypes().ToList();
            bool isCustom;
            if (item.GetType().BaseType.Name == "Array")
            {
                T[] itemToCheck = item as T[];
                isCustom = !_systemTypes.Contains( itemToCheck[0].GetType());
            }
            else
                isCustom = !_systemTypes.Contains(item.GetType());

            return isCustom;
        }
    }
}
