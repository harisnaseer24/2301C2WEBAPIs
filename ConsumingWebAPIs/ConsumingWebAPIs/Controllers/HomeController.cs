using System.Diagnostics;
using System.Net;

using ConsumingWebAPIs.Models;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace ConsumingWebAPIs.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
       public IActionResult Products()
        {
            string url = "https://localhost:7258/api/Product";


            using (WebClient web = new WebClient())
            {
                // Download JSON data as a string

                string jsonStr = web.DownloadString(url);

                // Deserialize JSON using Newtonsoft.Json (Json.NET)
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonStr);

                // Return the list of deserialized objects to the view
                return View(products);
            }


         
        }
    }
}
