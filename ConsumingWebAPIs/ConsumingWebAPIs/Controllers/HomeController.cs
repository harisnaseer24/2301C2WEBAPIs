using System.Diagnostics;
using System.Net;

using ConsumingWebAPIs.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public List<Brand> GetBrands() {


            string url = "https://localhost:7258/api/Brand";
            using (WebClient web = new WebClient())
            {
                string jsonStr = web.DownloadString(url);
                List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(jsonStr);
                return brands;
            }


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

        [HttpGet]
        public IActionResult AddProduct()
        {   
            List<Brand> brands = GetBrands();
             ViewBag.Brands = brands;
            //return View();
            return View();
        }
         [HttpPost]
        public IActionResult AddProduct(Product prd)
        {
                try
                {
                    using (var webClient = new WebClient())
                    {
                        // Convert the product object to JSON
                        string prdJson = JsonConvert.SerializeObject(prd);

                        // Set the content type to JSON
                        webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                        string response = webClient.UploadString("https://localhost:7258/api/Product", "POST", prdJson);
                        return RedirectToAction("Products");
                    }
                }
                catch (WebException ex)
                {
                    return View("Error", ex.Message);
                }
            
        }


         
        }
    }

