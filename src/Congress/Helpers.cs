﻿using Newtonsoft.Json;
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
                AmendmentId = new StringFilter("samdt2921-114", StringFilter.Options.All),
                Congress = new IntFilter(114),
                Number = new IntFilter(2921),
                Chamber = new StringFilter("senate"),
                AmendmentType = new StringFilter("samdt"),
                IntroducedOn = new DateTimeFilter(new DateTime(2015, 12, 7), DateTimeFilter.Options.GreaterThan),
                SponsorType = new StringFilter("person"),
                SponsorId = new StringFilter("C001070"),
                AmendsBillId = new StringFilter("sres207-114")
            }).ToArray();

            // Bill All
            Bill[] c = Bill.All().ToArray();

            // Bill Filter
            Bill[] d = Bill.Filter(new Bill.Filters() {
                BillId = new StringFilter("hr4193-114"),
                BillType = new StringFilter("hr"),
                Chamber = new StringFilter("house"),
                CommitteeIds = new StringFilter("HSII"),
                Congress = new IntFilter(114),
                CoSponsorsCount = new IntFilter(0),
                History = new HistoryFilter() {
                    Active = false,
                    AwaitingSignature = false,
                    Enacted = false,
                    Vetoed = false
                },
                IntroducedOn = new DateTimeFilter(new DateTime(2015, 12, 8)),
                Number = new IntFilter(4193),
                SponsorId = new StringFilter("Y000033")
            }).ToArray();

            // Bill Search
            Bill[] e = Bill.Search("To authorize the expansion of an existing hydroelectric project.").ToArray();

            // Committee All
            Committee[] f = Committee.All().ToArray();

            // Committee Filter
            Committee[] g = Committee.Filter(new Committee.Filters() {
                Chamber = new StringFilter("senate"),
                CommitteeId = new StringFilter("SSGA19"),
                ParentCommitteeId = new StringFilter("SSGA"),
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
                Chamber = new StringFilter("senate"),
                Congress = new IntFilter(114),
                LegislativeDay = new DateTimeFilter(new DateTime(2015, 12, 9))
            }).ToArray();

            // Hearing All
            Hearing[] n = Hearing.All().ToArray();

            // Hearing Filter 
            Hearing[] o = Hearing.Filter(new Hearing.Filters() {
                CommitteeId = new StringFilter("HSSM"),
                Chamber = new StringFilter("house"),
                Dc = true,
                Congress = new IntFilter(114),
                HearingType = new StringFilter("Hearing")
            }).ToArray();

            // Legislator All
            Legislator[] p = Legislator.All().ToArray();

            // Legislator Locate by Zip
            Legislator[] q = Legislator.Locate(60657).ToArray();

            // Legislator Locate by Lat/Long
            Legislator[] r = Legislator.Locate(42.96, -108.09).ToArray();

            // Legislator Filter
            Legislator[] s = Legislator.Filter(new Legislator.Filters() {
                BioguideID = new StringFilter("L000585"),
                Birthday = new DateTimeFilter(new DateTime(1968, 7, 4), DateTimeFilter.Options.GreaterThan),
                Chamber = new StringFilter("house"), 
                CrpId = new StringFilter("N00037031"),
                District = new IntFilter(18),
                FecIds = new StringFilter(new string[] { "H6IL18088" }),
                FirstName = new StringFilter("Darin"),
                Gender = new StringFilter("M"),
                GovTrackId = new StringFilter("412674"),
                InOffice = true,
                LastName = new StringFilter("LaHood"),
                Party = new StringFilter("R"),
                State = new StringFilter("IL"),
                VoteSmartId = new IntFilter(128760)
            }).ToArray();

            // Nomination All
            Nomination[] t = Nomination.All().ToArray();

            // Nomination Filter
            Nomination[] u = Nomination.Filter(new Nomination.Filters() {
                NominationId = new StringFilter("PN951-02-114"),
                Congress = new IntFilter(114),
                Number = new StringFilter("951-02"),
                Organization = new StringFilter("Foreign Service"),
                CommitteeIds = new StringFilter("SSFR"),
                LastActionAt = new DateTimeFilter(new DateTime(2015, 11, 19))
            }).ToArray();

            // Upcoming Bill All
            UpcomingBill[] v = UpcomingBill.All().ToArray();

            // Upcoming Bill Filter
            UpcomingBill[] w = UpcomingBill.Filter(new UpcomingBill.Filters() {
                BillId = new StringFilter("s1177-114"),
                Chamber = new StringFilter("senate"),
                Congress = new IntFilter(114),
                LegislativeDay = new DateTimeFilter(new DateTime(2015, 12, 9)),
                Range = new StringFilter("day"),
                SourceType = new StringFilter("senate_daily")
            }).ToArray();

            // Vote All
            Vote[] x = Vote.All().ToArray();

            // Vote Filter
            Vote[] y = Vote.Filter(new Vote.Filters() {
                BillId = new StringFilter("hr2130-114"),
                Chamber = new StringFilter("house"),
                Congress = new IntFilter(114),
                Number =new IntFilter(684),
                Required = new StringFilter("1/2"),
                RollId = new StringFilter("h684-2015"),
                VoteType = new StringFilter("amendment")
            }).ToArray();

            string result = string.Format(
                "a: {0}</p><p> b: {1}</p><p> c: {2}</p><p> d: {3}</p><p> e: {4}</p><p> f: {5}</p><p> g: {6}</p><p> h: {7}</p><p> i: {8}</p><p> j: {9}</p><p> k: {10}</p><p> l: {11}</p><p> m: {12}</p><p> n: {13}</p><p> o: {14}</p><p> p: {15}</p><p> q: {16}</p><p> r: {17}</p><p> s: {18}</p><p> t: {19}</p><p> u: {20}</p><p> v: {21}</p><p> w: {22}</p><p> x: {23}</p><p> y: {24}",
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
            using (WebClient client = new WebClient())
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
                    if (IsCustomFilter(value))
                        url = ExtractPropertiesOnCustomFilters(key.PropertyName, value, url, value.GetType());
                    else if (IsCustomClass(value))
                        url = ExtractPropertiesOnObjects(key.PropertyName, value, url);
                    else
                        url += string.Format("&{0}={1}", key.PropertyName, Helpers.ConvertToSafeString(value));
                }   
            }
            return url;
        }
        
        public static string ExtractPropertiesOnCustomFilters(string originalKey, object value, string url, Type type)
        {
            if (value.GetType() == typeof(StringFilter))
            {
                StringFilter castVal = value as StringFilter;
                if (castVal.Values != null)
                {
                    url += "&" + originalKey;
                    if (castVal.GreaterThan == true)
                        url += "__gt=";
                    else if (castVal.GreaterThanOrEquals == true)
                        url += "__gte=";
                    else if (castVal.LessThan == true)
                        url += "__lt=";
                    else if (castVal.LessThanOrEquals == true)
                        url += "__lte=";
                    if (castVal.All == true)
                        url += "__all=";
                    else if (castVal.In == true)
                        url += "__in=";
                    else if (castVal.Not == true)
                        url += "__not=";
                    else if (castVal.NotIn == true)
                        url += "__nin=";
                    else if (castVal.Exists == true)
                        url += "__exists=true";
                    else if (castVal.Exists == false)
                        url += "__exists=false";
                    else
                        url += "=";
                    if (castVal.Values.Length > 1)
                        foreach (string val in castVal.Values)
                            url += Helpers.ConvertToSafeString(val)+ "|";
                    else
                        url += Helpers.ConvertToSafeString(castVal.Values[0]);
                }
            }
            else if (value.GetType() == typeof(DateTimeFilter))
            {
                DateTimeFilter castVal = value as DateTimeFilter;
                if (castVal.Values != null)
                {
                    url += "&" + originalKey;
                    if (castVal.GreaterThan == true)
                        url += "__gt=";
                    else if (castVal.GreaterThanOrEquals == true)
                        url += "__gte=";
                    else if (castVal.LessThan == true)
                        url += "__lt=";
                    else if (castVal.LessThanOrEquals == true)
                        url += "__lte=";
                    else if (castVal.All == true)
                        url += "__all=";
                    else if (castVal.In == true)
                        url += "__in=";
                    else if (castVal.Not == true)
                        url += "__not=";
                    else if (castVal.NotIn == true)
                        url += "__nin=";
                    else if (castVal.Exists == true)
                        url += "__exists=true";
                    else if (castVal.Exists == false)
                        url += "__exists=false";
                    else
                        url += "=";
                    if (castVal.Values.Length > 1)
                        foreach (DateTime val in castVal.Values)
                            url += Helpers.ConvertToSafeString(val) + "|";
                    else
                        url += Helpers.ConvertToSafeString(castVal.Values[0]);
                }
                
            }
            else if (value.GetType() == typeof(IntFilter))
            {
                IntFilter castVal = value as IntFilter;
                if(castVal.Values != null)
                {
                    url += "&" + originalKey;
                    if (castVal.GreaterThan == true)
                        url += "__gt=";
                    else if (castVal.GreaterThanOrEquals == true)
                        url += "__gte=";
                    else if (castVal.LessThan == true)
                        url += "__lt=";
                    else if (castVal.LessThanOrEquals == true)
                        url += "__lte=";
                    else if (castVal.All == true)
                        url += "__all=";
                    else if (castVal.In == true)
                        url += "__in=";
                    else if (castVal.Not == true)
                        url += "__not=";
                    else if (castVal.NotIn == true)
                        url += "__nin=";
                    else if (castVal.Exists == true)
                        url += "__exists=true";
                    else if (castVal.Exists == false)
                        url += "__exists=false";
                    else
                        url += "=";
                    if (castVal.Values.Length > 1)
                        foreach (int val in castVal.Values)
                            url += Helpers.ConvertToSafeString(val) + "|";
                    else
                        url += Helpers.ConvertToSafeString(castVal.Values[0]);
                }   
            }
            
            return url;
        }

        public static string ExtractPropertiesOnObjects<T>(string originalKey, T value, string url)
        {
            var props = value.GetType().GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                if (IsCustomFilter(value)) // if this sub-property is a Filter class
                    url = ExtractPropertiesOnCustomFilters(originalKey, value, url, value.GetType());
                else // just a normal system type
                {
                    JsonPropertyAttribute key = value.GetType().GetProperty(props[i].Name).GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0] as JsonPropertyAttribute;
                    var subValue = value.GetType().GetProperty(props[i].Name).GetValue(value, null);
                    if (subValue != null && !string.IsNullOrEmpty(subValue.ToString()))
                    {
                        url += string.Format("&{0}.{1}={2}", originalKey, key.PropertyName, Helpers.ConvertToSafeString(subValue));
                    }
                }
            }
            return url;
        }

        public static bool IsCustomFilter<T>(T item)
        {
            bool isCustom = false;
            if (item.GetType().BaseType.Name == "Filter")
                isCustom = true;
            return isCustom;
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

        public abstract class Base
        {
            public abstract void Use();
            public abstract object GetProp();
        }

        public abstract class GenericBase<T> : Base
        {
            public T Prop { get; set; }

            public override object GetProp()
            {
                return Prop;
            }
        }

        public class DateReceive : GenericBase<DateTime>
        {
            public override void Use() { }
        }

        public class DateSend : GenericBase<Tuple<DateTime, string>>
        {
            public override void Use() { }
        }
    }
}
