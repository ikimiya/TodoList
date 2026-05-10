using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("TodoListAPI");
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonSerializer.Serialize(new { email = model.Email, password = model.Password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<JsonElement>(responseBody)
                .GetProperty("token").GetString();

            HttpContext.Session.SetString("JwtToken", token!);
            return RedirectToAction("Index", "Tasks");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonSerializer.Serialize(new { email = model.Email, password = model.Password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Email already exists");
                return View(model);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JwtToken");
            return RedirectToAction("Login");
        }
    }
}