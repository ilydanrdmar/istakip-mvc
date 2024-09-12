using iştakip.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using iştakip.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace iştakip.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()

        {
            ViewBag.mesaj = null;
            return View();
        }

        public async Task <IActionResult> Index(Personel p)
        {
            
            var personel = _context.Personels.FirstOrDefault(x => x.PersonelKullanıcıAd == p.PersonelKullanıcıAd && x.Password==p.Password);
            if (personel != null)
            {

                var claims = new List<Claim>
                {
                   // new Claim(ClaimTypes.Name,p.PersonelKullanıcıAd),
                   
                    //sonradan 4 tane sey ekledim

                        new Claim(ClaimTypes.Name, personel.PersonelKullanıcıAd ?? "UnknownUser"),
                        new Claim("PersonelAdSoyad", personel.PersonelAdSoyad ?? "Unknown Name"),
                        new Claim("PersonelId", personel.PersonelId.ToString() ?? "0"),  // int? to string conversion
                        new Claim("PersonelYetkiTurId", personel.PersonelYetkiTurId?.ToString() ?? "0"),  // int? to string conversion
                       new Claim("PersonelBirimId", personel.PersonelBirimId?.ToString() ?? "0")  // int? to string conversion


                    //new Claim("PersonelAdSoyad", p.PersonelAdSoyad),
                    //new Claim("PersonelId", p.PersonelId.ToString()),  // Personel Id
                    //new Claim("PersonelYetkiTurId", p.PersonelYetkiTurId.ToString()),  // Yetki Tur Id
                    //new Claim("PersonelBirimId", p.PersonelBirimId.ToString())
                };
                var useridentity=new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal=new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);

                switch (personel.PersonelYetkiTurId)
                {
                    case 1:
                        return RedirectToAction("Index", "Yönetici");
                    case 2:
                        return RedirectToAction("Index", "Calisan");
                    default:
                        return View(); // Yetki türü tanımlanmadıysa geri dön
                }
            }
            //sonradan ekledim.
            else
            {
                ViewBag.mesaj = "Kullanıcı adı veya parola yanlış";
                return View();
            }
            
            return View();



        }






    }
}
