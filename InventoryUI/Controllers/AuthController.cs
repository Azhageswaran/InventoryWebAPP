using InventoryUI.Models.Dtos.RawMaterialsDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using InventoryUI.Models.Dtos.AuthDtos;

namespace InventoryUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginRequestDto model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7167/api/Auth/Login"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<TokenResponseDto>();
            HttpContext.Session.SetString("JWTToken", response.JwtToken);
            if (response is not null)
            {
                return RedirectToAction("Index", "RawMaterials");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("LoginUser", "Auth");
        }

    }
}
