using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Congress
{
    internal class Helpers
    {

        internal static SunlightResponse<T> Get<T>(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.BaseAddress = url;
                string response = client.DownloadString(client.BaseAddress);
                return JsonConvert.DeserializeObject<SunlightResponse<T>>(response);
            }
        }

        internal static string QueryString<T>(string url, T filters)
        {
            string parameters = ExtractPropertiesOnObjects("", filters);
            return url += string.Format("&{0}", string.Join("&", parameters));
        }

        internal static string ExtractPropertiesOnObjects<T>(string originalKey, T value)
        {
            var props = value.GetType().GetProperties();
            List<string> keys = new List<string>();
            for (int i = 0; i < props.Length; i++)
            {
                JsonPropertyAttribute singleKey = value.GetType().GetProperty(props[i].Name).GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0] as JsonPropertyAttribute;
                var subValue = value.GetType().GetProperty(props[i].Name).GetValue(value, null);
                List<string> subKeys = new List<string>();
                if (subValue != null && !string.IsNullOrEmpty(subValue.ToString()))
                {
                    string singleSubKey = !string.IsNullOrEmpty(originalKey) ? string.Format("{0}.{1}", originalKey, singleKey.PropertyName) : singleKey.PropertyName;
                    if (subValue.GetType().BaseType.Name == "Filter`1")                                         // property is filter<type> class, so extract value and operator
                        subKeys.Add(ExtractPropertiesOnCustomFilters(singleSubKey, subValue));
                    else if (IsCustomClass(subValue))                                                           // property is custom class, so extract properties
                        subKeys.Add(ExtractPropertiesOnObjects(singleSubKey, subValue));
                    else                                                                                        // property is normal system class, so format as normal
                        subKeys.Add(string.Format("{0}={1}", singleSubKey, Helpers.ConvertToSafeString(subValue)));
                    keys.Add(string.Join("&", subKeys));                                                        // then add sub-key to keys list
                }
            }                   
            return string.Join("&", keys);                                                                      // join all the keys together with an ampersand
        }

        internal static string ExtractPropertiesOnCustomFilters(string originalKey, object value)
        {
            if (value.GetType() == typeof(StringFilter))
            {
                StringFilter castVal = value as StringFilter;
                if (castVal.Values != null)
                {
                    if (!string.IsNullOrEmpty(castVal.Operator))
                        originalKey += string.Format("__{0}", castVal.Operator);
                    else
                        originalKey += "=";
                    originalKey += ConvertToSafeString(string.Join("|", castVal.Values));
                }
            }
            else if (value.GetType() == typeof(DateFilter))
            {
                DateFilter castVal = value as DateFilter;
                if (castVal.Values != null)
                {
                    if (!string.IsNullOrEmpty(castVal.Operator))
                        originalKey += string.Format("__{0}", castVal.Operator);
                    else
                        originalKey += "=";
                    originalKey += ConvertToSafeString(string.Join("|", castVal.Values));
                }

            }
            else if (value.GetType() == typeof(IntFilter))
            {
                IntFilter castVal = value as IntFilter;
                if (castVal.Values != null)
                {
                    if (!string.IsNullOrEmpty(castVal.Operator))
                        originalKey += string.Format("__{0}", castVal.Operator);
                    else
                        originalKey += "=";
                    originalKey += ConvertToSafeString(string.Join("|", castVal.Values));
                }
            }

            return originalKey;
        }

        private static List<Type> _systemTypes;
        internal static bool IsCustomClass<T>(T item)
        {
            if (_systemTypes == null)
                _systemTypes = Assembly.GetExecutingAssembly().GetType().Module.Assembly.GetExportedTypes().ToList();
            bool isCustom;
            if (item.GetType().BaseType.Name == "Array")
            {
                T[] itemToCheck = item as T[];
                isCustom = !_systemTypes.Contains(itemToCheck[0].GetType());
            }
            else
                isCustom = !_systemTypes.Contains(item.GetType());

            return isCustom;
        }

        internal static string ConvertToSafeString<T>(T prop)
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
                return string.Format("%22{0}%22", prop.ToString());
        }
    }
}
