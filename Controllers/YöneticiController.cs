using iştakip.Data;
using iştakip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
namespace iştakip.Controllers
{
    public class YöneticiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YöneticiController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var PersonelYetkiTurIdClaim = User.FindFirst("PersonelYetkiTurId")?.Value;
            int yetkiTurId = Convert.ToInt32(PersonelYetkiTurIdClaim);

            if (yetkiTurId == 1)
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
            return View();

            // Kullanıcının claim'lerinden BirimId'yi al
            //var birimIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BirimId");
            //if (birimIdClaim != null)
            //{
            //  int birimId = int.Parse(birimIdClaim.Value);

            // Birim bilgilerini al
            // var birim = await _context.Birims
            // .Where(b => b.BirimId == birimId)
            // .Select(b => new { b.BirimAd, b.BirimId })
            // .FirstOrDefaultAsync();

            //return View();
            // }
            // return View();
        }

        public async Task<IActionResult> Ata()
        {
            var PersonelYetkiTurIdClaim = User.FindFirst("PersonelYetkiTurId")?.Value;
            int yetkiTurId = Convert.ToInt32(PersonelYetkiTurIdClaim);

            if (yetkiTurId == 1)
            {
                var PersonelBirimIdClaim = User.FindFirst("PersonelBirimId")?.Value;

                if (!int.TryParse(PersonelBirimIdClaim, out int birimId))
                {
                    return RedirectToAction("Index", "Login");
                }

                var calisanlar = await _context.Personels
                    .Where(p => p.PersonelBirimId == PersonelBirimIdClaim && p.PersonelYetkiTurId == 2)
                    .ToListAsync();
                ViewBag.personeller = calisanlar;




                // Claim değeri null değilse, int'e dönüştürüyoruz.

                //var birim = (from b in _context.Birims where b.BirimId == birimId select b).FirstOrDefault();
                var birim = await _context.Birims
                    .Where(b => b.BirimId == birimId)
                    .Select(b => new { b.BirimAd, b.BirimId })
                    .FirstOrDefaultAsync();
                ViewBag.birimAd = birim.BirimAd;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Ata(AtaViewModel model)
        {
            var baslik = model.IsBaslık;
            var aciklama = model.IsAcıklama;

            // Seçili personel ID'sini string olarak al
            var selectedPersonelId = model.SelectedPersonelId;


            // Yeni 'Is' nesnesini oluştur
            var yeniIs = new Is
            {
                IsBaslık = model.IsBaslık,
                IsAcıklama = model.IsAcıklama,
                IsPersonelId = selectedPersonelId, // Dönüştürülen integer değer
                BaslangicTarih = DateTime.Now, // Şu anki tarih
                IsDurumId = 1,
            };

            // Veritabanına ekle
            _context.Iss.Add(yeniIs);
            await _context.SaveChangesAsync();

            return RedirectToAction("Takip", "Yönetici");


            // Eğer dönüştürme başarısız olursa formu tekrar gösterin
            return View(model);
        }



        public async Task<IActionResult> Takip()
        {
            var PersonelYetkiTurIdClaim = User.FindFirst("PersonelYetkiTurId")?.Value;
            int yetkiTurId = Convert.ToInt32(PersonelYetkiTurIdClaim);

            if (yetkiTurId == 1)
            {
                var PersonelBirimIdClaim = User.FindFirst("PersonelBirimId")?.Value;

                if (!int.TryParse(PersonelBirimIdClaim, out int birimId))
                {
                    return RedirectToAction("Index", "Login");
                }

                var calisanlar = await _context.Personels
                    .Where(p => p.PersonelBirimId == PersonelBirimIdClaim && p.PersonelYetkiTurId == 2)
                    .ToListAsync();
                ViewBag.personeller = calisanlar;




                // Claim değeri null değilse, int'e dönüştürüyoruz.

                //var birim = (from b in _context.Birims where b.BirimId == birimId select b).FirstOrDefault();
                var birim = await _context.Birims
                    .Where(b => b.BirimId == birimId)
                    .Select(b => new { b.BirimAd, b.BirimId })
                    .FirstOrDefaultAsync();
                ViewBag.birimAd = birim.BirimAd;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        // Takip aksiyonu
        public IActionResult Takip(int selectedPersonelId)
        {
            // Seçilen personelin bilgilerini al
            var secilenPersonel = _context.Personels
                .FirstOrDefault(p => p.PersonelId == selectedPersonelId);

            if (secilenPersonel != null)
            {
                // Personel bilgisini JSON formatında serileştir
                var secilenPersonelJson = JsonConvert.SerializeObject(secilenPersonel);
                TempData["PersonelData"] = secilenPersonelJson;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

            // Listele aksiyonuna yönlendir
            return RedirectToAction("Listele", "Yönetici");
        }


        // Listele aksiyonu
        public IActionResult Listele()
        {
            var PersonelYetkiTurIdClaim = User.FindFirst("PersonelYetkiTurId")?.Value;
            if (int.TryParse(PersonelYetkiTurIdClaim, out int yetkiTurId) && yetkiTurId == 1)
            {
                // TempData'dan JSON formatında personel bilgisini al
                var personelDataJson = TempData["PersonelData"] as string;
                if (personelDataJson != null)
                {
                    var secilenPersonel = JsonConvert.DeserializeObject<Personel>(personelDataJson);

                    var isler = _context.Iss
                        .Where(i => i.IsPersonelId == secilenPersonel.PersonelId)
                        .ToList().OrderByDescending(i =>i.BaslangicTarih);

                    ViewBag.Isler = isler;
                    ViewBag.Personel = secilenPersonel;
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

    }
}