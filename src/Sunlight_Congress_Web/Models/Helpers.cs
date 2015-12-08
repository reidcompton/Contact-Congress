using Newtonsoft.Json;
using Sunlight_Congress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Sunlight_Congress
{
    public class Helpers
    {
        public static string ConvertToSafeString<T>(T prop)
        {
            if (prop.GetType() == typeof(DateTime))
                return ((DateTime)(object)prop).ToString("yyyy-MM-dd");
            else
                return prop.ToString().ToLower();
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
                    if(IsCustomClass(value))
                    {
                        url = ExtractProperties(key.PropertyName, value, url);
                    }
                    else
                        url += string.Format("&{0}={1}", key.PropertyName, Helpers.ConvertToSafeString(value));
                }   
            }
            return url;
        }

        public static string ExtractProperties<T>(string originalKey, T value, string url)
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
            return !_systemTypes.Contains(item.GetType());
        }
    }
}
