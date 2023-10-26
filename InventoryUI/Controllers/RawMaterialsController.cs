using InventoryUI.Models.Dtos.RawMaterialsDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
                var client = httpClientFactory.CreateClient();

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
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7167/api/RawMaterials"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8,"application/json")
            };

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
            var client = httpClientFactory.CreateClient();
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
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7167/api/RawMaterials/{request.RawMaterialId}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

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
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7167/api/RawMaterials/{request.RawMaterialId}");

                httpResponseMessage.EnsureSuccessStatusCode() ;

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {

                //console
            }

            return View("Edit");
        }
    }
}
