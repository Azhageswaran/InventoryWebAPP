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
            try
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
                HttpContext.Session.SetString("Roles", response.Roles);

                // ViewData["Roles"] = response.Roles;
                /* TempData["Roles"] = response.Roles;

                 // Check user roles
                 var isAdmin = User.IsInRole("Admin");
                 var isStaff = User.IsInRole("Staff");
 */
                // var isRoles = (response.Roles == "Admin") ? "Admin" : "Staff";
                var isRoles = HttpContext.Session.GetString("Roles");
               
            

                if (response is not null)
                {
                      return RedirectToAction("Index", "RawMaterials", new { isRoles});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
