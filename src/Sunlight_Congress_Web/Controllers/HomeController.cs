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
using System.Net.Mail;

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

        public static SmtpClient InitSendGridClient()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.sendgrid.net";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(Settings.SendGrid.Username, Settings.SendGrid.Password);
            return client;
        }

        public static MailMessage InitSendGridMessage(string toName, string fromName, string fromAddress, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            foreach (string to in toName.Split(','))
            {
                mail.To.Add(new MailAddress(to));
            }
            mail.From = new MailAddress(fromName + "<" + fromAddress + ">");
            mail.Subject = subject;
            mail.Body = body;
            return mail;
        }

        public JsonResult SendEmail(string toEmails, string fromName, string fromEmail, string subject, string body)
        {
            subject = subject.Substring(0, subject.Length <= 78 ? subject.Length : 78);
            SmtpClient client = InitSendGridClient();
            MailMessage mail = InitSendGridMessage(toEmails, fromName, fromEmail, subject, body);
            string result = "";
            try
            {
                client.Send(mail);
                result = "success";
            }
            catch
            {
                result = "failed";
            }
            return Json(result);
        }

        public JsonResult Legislators(string address)
        {
            GoogleGeocoder geocoder = new GoogleGeocoder(Settings.GoogleMapsApiKey);
            Address response = geocoder.Geocode(address).First();
            Congress.Congress client = new Congress.Congress(Settings.SunlightCongressApiKey);
            Legislator[] legislators = client.Legislators.Where(x => x.Latitude == response.Coordinates.Latitude && x.Longitude == response.Coordinates.Longitude).ToArray();
            
            return Json(legislators);
        }

        public JsonResult LegislatorsByLatLong(double longitude, double latitude)
        {
            Congress.Congress client = new Congress.Congress(Settings.SunlightCongressApiKey);
            Legislator[] legislators = client.Legislators.Where(x => x.Latitude == longitude && x.Longitude == latitude).ToArray();

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
