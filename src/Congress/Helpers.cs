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
            for (int i = 0; i < props.Length; i++)
            {
                JsonPropertyAttribute key = filters.GetType().GetProperty(props[i].Name).GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0] as JsonPropertyAttribute;
                var value = filters.GetType().GetProperty(props[i].Name).GetValue(filters, null);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    if (value.GetType().BaseType.Name == "Filter")          // is filter class
                        url = ExtractPropertiesOnCustomFilters(key.PropertyName, value, url, value.GetType());
                    else if (IsCustomClass(value))                          // is custom class 
                        url = ExtractPropertiesOnObjects(key.PropertyName, value, url);
                    else                                                    // is normal system class
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
                if (value.GetType().BaseType.Name == "Filter")              // is filter class
                    url = ExtractPropertiesOnCustomFilters(originalKey, value, url, value.GetType());
                else                                                        // is normal system type
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
                            url += ConvertToSafeString(val)+ "|";
                    else
                        url += ConvertToSafeString(castVal.Values[0]);
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
                            url += ConvertToSafeString(val) + "|";
                    else
                        url += ConvertToSafeString(castVal.Values[0]);
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
                            url += ConvertToSafeString(val) + "|";
                    else
                        url += ConvertToSafeString(castVal.Values[0]);
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
                return string.Format("%22{0}%22", prop.ToString());
        }
    }
}
