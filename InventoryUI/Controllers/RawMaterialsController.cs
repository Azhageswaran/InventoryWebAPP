using InventoryUI.Models.Dtos.RawMaterialsDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace InventoryUI.Controllers
{
    public class RawMaterialsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RawMaterialsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async  Task<IActionResult> Index()
        {
            List<RawMaterialsDtos> response = new List<RawMaterialsDtos>();
            try
            {
                //AccessToken
                var accessToken = HttpContext.Session.GetString("JWTToken");

                var client = httpClientFactory.CreateClient();

                //Token Header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var httpResponseMessage = await client.GetAsync("https://localhost:7167/api/RawMaterials/SPGetAll");
                
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RawMaterialsDtos>>());
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRawMaterialsViewModel model)
        {
            //Session AccessToken
            var accessToken = HttpContext.Session.GetString("JWTToken");

            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7167/api/RawMaterials"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            //Token Authorization
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RawMaterialsDtos>();
            if(response is not null)
            {
                return RedirectToAction("Index", "RawMaterials");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Session AccessToken
            var accessToken = HttpContext.Session.GetString("JWTToken");

            var client = httpClientFactory.CreateClient();

            //Token In Header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetFromJsonAsync<RawMaterialsDtos>($"https://localhost:7167/api/RawMaterials/{id.ToString()}");
            if(response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RawMaterialsDtos request)
        {
            //Session AccessToken
            var accessToken = HttpContext.Session.GetString("JWTToken");

            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7167/api/RawMaterials/{request.RawMaterialId}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            //Bearer Header 
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RawMaterialsDtos>();
            if(response is not null)
            {
                return RedirectToAction("Index", "RawMaterials");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RawMaterialsDtos request)
        {
            try
            {
                //Session AccessToken
                var accessToken = HttpContext.Session.GetString("JWTToken");

                var client = httpClientFactory.CreateClient();

                //Token In Header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7167/api/RawMaterials/{request.RawMaterialId}");

                httpResponseMessage.EnsureSuccessStatusCode() ;

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                return View("Edit");
                //console
            }

          
        }
    }
}
