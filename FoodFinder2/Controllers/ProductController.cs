using FoodFinder2.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodFinder2.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> MakeRequest(string q)// worked 
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "1f018fcb123847b182d07573e0813f5f");

            var uri = string.Format("https://dev.tescolabs.com/grocery/products/?query={0}&offset={1}&limit={2}", q, 0, 100);

            var response = await client.GetAsync(uri);
            string body = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(body);

            IList<JToken> results = result["uk"]["ghs"]["products"]["results"].Children().ToList();

            //// serialize JSON results into .NET objects
            IList<Product> products = new List<Product>();
            foreach (JToken r in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                Product product = r.ToObject<Product>();
                products.Add(product);
            }
            return View(products);
        }

    }
}
