using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace iştakip.Controllers
{
    public class LogoutController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Kullanıcının oturumunu kapatır ve tüm kimlik bilgilerini temizler
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Giriş sayfasına yönlendirir
            return RedirectToAction("Index", "Login");
        }
    }
}
