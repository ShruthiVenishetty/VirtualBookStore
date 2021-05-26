using BookList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }
        private HttpRequestMessage request;
        private object httpClient;

        // private object request;

        // GET: BooksController
        public async Task<ActionResult> Index()
        {
            List<Books> b = new List<Books>();
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://localhost:44342/GetAllBooks"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        b = JsonConvert.DeserializeObject<List<Books>>(result);
                    }
                }
            }
            return View(b);
        }
        public async Task<ActionResult> GetBookbyName(string name)
        {
            Books b = new Books();
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://localhost:44342/GetBookbyName?name="+name))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        b = JsonConvert.DeserializeObject<Books>(result);
                    }
                }
            }
            return View(b);

        }
        public ActionResult AddBook()
        {
           return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(Books b)
        {
            b.AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:44342/AddABook?BookName=" + b.BookName + "&Author=" + b.Author + "&Category=" + b.Category + "&Subcategory=" + b.Subcategory + "&Publisher=" + b.Publisher + "&Price=" + b.Price + "&IsActive=" + b.IsActive + "&Pages=" + b.Pages + "&AddedBy=" + b.AddedBy))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var str = Newtonsoft.Json.JsonConvert.SerializeObject(b);

                    request.Content = new StringContent(str);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/Json");

                    var response = await httpClient.SendAsync(request);
                    
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delisted()
        {
            List<Books> b = new List<Books>();
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://localhost:44342/DelistedBooks"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");


                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        b = JsonConvert.DeserializeObject<List<Books>>(result);
                    }
                }
            }
            return View(b);
        }
        public async Task<ActionResult> Delete(string name)
        {
            
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), "https://localhost:44342/Deletebyname?name="+name))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var response = await httpClient.SendAsync(request);

                }
                
            }
            return RedirectToAction("Index");
        } 
        public async Task<ActionResult> Delist(string name)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), "https://localhost:44342/DelistByName?name="+name))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var response = await httpClient.SendAsync(request);

                }
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Enlist(string name)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), "https://localhost:44342/EnlistByName?name="+name))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var response = await httpClient.SendAsync(request);

                }
            }
            return RedirectToAction("Delisted");
        }
        public async Task<ActionResult> Edit(string name)
        {
            
            Books b = new Books();
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://localhost:44342/GetBookbyName?name=" + name))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        b = JsonConvert.DeserializeObject<Books>(result);
                    }
                }
            }
            
            return View(b);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Books b)
        {
            b.ModifiedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (var httpClient = new HttpClient())
            {
                
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), "https://localhost:44342/EditBookByName?name="+b.BookName))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    var str = Newtonsoft.Json.JsonConvert.SerializeObject(b);

                    request.Content = new StringContent(str);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");


                    var response = await httpClient.SendAsync(request);
                    
                 
                }
            }
 
            return RedirectToAction("Index");
        }
    }
}
