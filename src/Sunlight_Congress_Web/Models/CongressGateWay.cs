using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Sunlight_Congress_Web.Models;

namespace Sunlight_Congress
{
    public class CongressGateWay
    {
        public static WebClient Connect(string baseURL, string token)
        {
            using (WebClient client = new WebClient())
            {
                client.BaseAddress = string.Format("{0}?apikey={1}", baseURL, token);
                return client;
            }
        }
    }
}
