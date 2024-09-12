using System.ComponentModel.DataAnnotations;

namespace iştakip.Models
{
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }
        public ICollection<Is>? Iss { get; set; }
        public string? PersonelAdSoyad { get; set; }
        public string? PersonelKullanıcıAd { get; set; }
        public string? Password { get; set; }
       
        public string? PersonelBirimId { get; set; }
        public  Birim? Birim { get; set; }

        public int? PersonelYetkiTurId{ get; set; }
        public  YetkiTur? YetkiTur { get; set; }


    }
}
