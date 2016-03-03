using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Congress;
using ContactCongress.Models;
using Geocoding;
using Geocoding.Google;

namespace Sunlight_Congress_Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            AddressModel model = new AddressModel();
            return View(model);
        }


        public IActionResult Test()
        {
            string result = Tests.RunTests();
            ViewBag.Result = result;
            return View();
        }

        public JsonResult Legislators(string address)
        {
            GoogleGeocoder geocoder = new GoogleGeocoder(Settings.GoogleMapsApiKey);
            Address response = geocoder.Geocode(address).First();
            Congress.Congress client = new Congress.Congress(Settings.SunlightCongressApiKey);
            Legislator[] legislators = client.Legislators.Where(x => x.Latitude == response.Coordinates.Latitude && x.Longitude == response.Coordinates.Longitude).ToArray();
            
            return Json(legislators);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
