using InventoryUI.Models.Dtos.MovedStocksDtos;
using InventoryUI.Models.Dtos.PurchasedStocksDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

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
                //AccessToken
                var accessToken = HttpContext.Session.GetString("JWTToken");

                var client = httpClientFactory.CreateClient();

                //Token Header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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
            try
            {
                addMovedStocksViewModel.RawMaterialId = id;

                //Session AccessToken
                var accessToken = HttpContext.Session.GetString("JWTToken");
                var isRoles = HttpContext.Session.GetString("Roles");

                var client = httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7167/api/MovedStocks"),
                    Content = new StringContent(JsonSerializer.Serialize(addMovedStocksViewModel), Encoding.UTF8, "application/json")
                };

                //Bearer Header 
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<MovedStocksDtos>();

                if (response is not null)
                {
                    return RedirectToAction("Index", "RawMaterials", new { isRoles });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return View();
        }
    }
}
