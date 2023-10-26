using InventoryUI.Models.Dtos.PurchasedStocksDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InventoryUI.Controllers
{
    public class PurchasedStocksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public PurchasedStocksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<PurchasedStocksDtos> response = new List<PurchasedStocksDtos>();
            try
            {
                //AccessToken
                var accessToken = HttpContext.Session.GetString("JWTToken");

                var client = httpClientFactory.CreateClient();

                //Token Header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var httpResponseMessage = await client.GetAsync("https://localhost:7167/api/PurchasedStocks/SPGetAllPurchasedStocks");

                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PurchasedStocksDtos>>());
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid id,AddPurchasedStocksViewModel addPurchasedStocksViewModel)
        {
            addPurchasedStocksViewModel.RawMaterialId = id;

            //Session AccessToken
            var accessToken = HttpContext.Session.GetString("JWTToken");

            var client = httpClientFactory.CreateClient();
            
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7167/api/PurchasedStocks"),
                Content = new StringContent(JsonSerializer.Serialize(addPurchasedStocksViewModel), Encoding.UTF8, "application/json")
            };

            //Bearer Header 
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<PurchasedStocksDtos>();

            if(response is not null)
            {
                return RedirectToAction("Index", "RawMaterials");
            }

            return View();
        }
    }
}
