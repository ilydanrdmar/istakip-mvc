using iştakip.Data;
using iştakip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace iştakip.Controllers
{
    //CLASS OLUSTURMA 
    public class IsDurum
    {
        public string IsBaslık { get; set; }
        public string? IsAcıklama { get; set; }
        public DateTime? BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }

        public string DurumAd { get; set; }
    }
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalisanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var PersonelYetkiTurIdClaim = User.FindFirst("PersonelYetkiTurId")?.Value;
            int yetkiTurId = Convert.ToInt32(PersonelYetkiTurIdClaim);


            if (yetkiTurId == 2)
            {
                var PersonelBirimIdClaim = User.FindFirst("PersonelBirimId")?.Value;

                // Claim değeri null değilse, int'e dönüştürüyoruz.
                int birimId = Convert.ToInt32(PersonelBirimIdClaim);
                //var birim = (from b in _context.Birims where b.BirimId == birimId select b).FirstOrDefault();
                var birim = await _context.Birims
                    .Where(b => b.BirimId == birimId)
                    .Select(b => new { b.BirimAd, b.BirimId })
                    .FirstOrDefaultAsync();
                ViewBag.birimAd = birim.BirimAd;
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public async Task<IActionResult> Yap()
        {


            var PersonelYetkiTurIdClaim = User.FindFirst("PersonelYetkiTurId")?.Value;
            int yetkiTurId = Convert.ToInt32(PersonelYetkiTurIdClaim);
            if (yetkiTurId == 2)
            {
                if (yetkiTurId == 2)
                {
                    var PersonelIdClaim = User.FindFirst("PersonelId")?.Value;
                    int personelId = Convert.ToInt32(PersonelIdClaim);

                    var isler = await _context.Iss
                     .Where(i => i.IsPersonelId == personelId && i.IsDurumId == 1)
                     .OrderByDescending(i => i.BaslangicTarih) // IsTarihi'ne göre azalan sırada sıralar
                     .ToListAsync();

                    ViewBag.isler = isler;
                }


            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Yap(int IsId)
        {
            var tekis = await _context.Iss
                .Where(i => i.IsId == IsId)
                .FirstOrDefaultAsync();
            //TEKİS NESNESİ UZERİNDE PROPERTYLERİ DEĞİŞTİRDİK.  
            tekis.BaslangicTarih = DateTime.Now;
            tekis.IsDurumId = 2;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Calisan");
        }

        public async Task<IActionResult> Takip()
        {
            var PersonelYetkiTurIdClaim = User.FindFirst("PersonelYetkiTurId")?.Value;
            int yetkiTurId = Convert.ToInt32(PersonelYetkiTurIdClaim);

            if (yetkiTurId == 2)
            {
                var PersonelIdClaim = User.FindFirst("PersonelId")?.Value;
                int personelId = Convert.ToInt32(PersonelIdClaim);

                var isler = await (from i in _context.Iss
                                   join d in _context.Durums on i.IsDurumId equals d.DurumId1
                                   where i.IsPersonelId == personelId
                                   orderby i.BaslangicTarih descending
                                   select new IsDurum
                                   {
                                       IsBaslık = i.IsBaslık,
                                       IsAcıklama = i.IsAcıklama,
                                       BaslangicTarih = i.BaslangicTarih,
                                       BitisTarih = i.BitisTarih, // Eğer gerekiyorsa
                                       DurumAd = d.DurumAd // Eğer gerekiyorsa
                                   })
                               .ToListAsync();

                // Modeli oluştur ve view'a gönder
                var model = new IsDurumModel
                {
                    IsDurums = isler
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}