using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LordOfCodes.Web.Controllers
{
    public class HomeController : Controller
    {
        public List<InstagramSchema> Items;
        public string Hastag;
        public ActionResult Index()
        {          
            return View();
        }

        public async Task<ActionResult> GetInstagramDataAction()
        {
            var appId = "4e9badaafa4e4436977733f01e05fbd0";
            this.Items = new List<InstagramSchema>();
            this.Hastag = "universityofsalamanca";

            Uri requestedUri = new Uri(string.Format("https://api.instagram.com/v1/tags/{0}/media/recent?client_id={1}", Hastag, appId), UriKind.Absolute);
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(requestedUri);
            var statusCode = response.StatusCode;
            string jsonData = await response.Content.ReadAsStringAsync();
            var parsedData = JsonConvert.DeserializeObject<InstagramResponse>(jsonData);
            if (parsedData != null)
            {
                Items.Clear();
                foreach (var item in parsedData.ToSchema())
                {
                    Items.Add(item);
                }
            }
            return View("Instagram", this.Items);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}