using InventoryUI.Models.Dtos.MovedStocksDtos;
using InventoryUI.Models.Dtos.PurchasedStocksDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace InventoryUI.Controllers
{
    public class MovedStocksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public MovedStocksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<MovedStocksDtos> response = new List<MovedStocksDtos>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7167/api/MovedStocks/SPGetMovedStocksAll");
                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<MovedStocksDtos>>());

            }
            catch (Exception ex)
            {

                throw;
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Move(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Move(Guid id, AddMovedStocksViewModel addMovedStocksViewModel)
        {
            addMovedStocksViewModel.RawMaterialId = id;
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7167/api/MovedStocks"),
                Content = new StringContent(JsonSerializer.Serialize(addMovedStocksViewModel), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<MovedStocksDtos>();

            if (response is not null)
            {
                return RedirectToAction("Index", "RawMaterials");
            }

            return View();
        }
    }
}
