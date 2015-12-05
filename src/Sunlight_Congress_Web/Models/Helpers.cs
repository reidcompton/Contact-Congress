using Newtonsoft.Json;
using Sunlight_Congress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sunlight_Congress_Web.Models
{
    public class Helpers
    {
        public static string ConvertToSafeString<T>(T prop)
        {
            if (prop.GetType() == typeof(DateTime))
                return ((DateTime)(object)prop).ToString("yyyy-MM-dd");
            else
                return prop.ToString();
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
    }
}
