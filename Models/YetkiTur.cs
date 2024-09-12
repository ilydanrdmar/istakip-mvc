using System.ComponentModel.DataAnnotations;

namespace iştakip.Models
{
    public class YetkiTur
    {
        [Key]
        public int YetkiTurId { get; set; }
        public string? YetkiTurAd { get; set; }

        public  ICollection<Personel>? Personels { get; set; }

    }
}
