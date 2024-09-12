using System.ComponentModel.DataAnnotations;

namespace iştakip.Models
{
    public class Birim
    {
        [Key]
        public int BirimId { get; set; }
        public string? BirimAd { get; set; }

        public  ICollection<Personel>? Personels { get; set; }

    }
}
