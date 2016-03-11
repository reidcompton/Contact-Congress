using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactCongress.Models
{
    public static class Settings
    {
        public static string GoogleMapsApiKey
        {
            get
            {
                return "Your Google Maps API Key";
            }
        }

        public static string SunlightCongressApiKey
        {
            get
            {
                return "Your Sunlight Congress API Key";
                // http://sunlightfoundation.com/api/
            }
        }

        public class SendGrid
        {
            public static string Username
            {
                get
                {
                    return "Your SendGrid Username";
                }
            }

            public static string Password
            {
                get
                {
                    return "Your SendGrid Password";
                }
            }
        }
    }
}
