using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    public class ItemListController : Controller
    {
        //public IActionResult Index2()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            { 
                return RedirectToAction("Login","Login");
            }
            else
            {
                List<ItemList> ItemList = new List<ItemList>();
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44321/api/ItemList"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ItemList = JsonConvert.DeserializeObject<List<ItemList>>(apiResponse);
                    }
                }
                return View(ItemList);
            }
        }
    }
}
