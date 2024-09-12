using System.ComponentModel.DataAnnotations;

namespace iştakip.Models
{
    public class Durum
    {
        [Key]
        public int DurumId1 { get; set; }
        public string? DurumAd{ get; set; }

        public ICollection<Is>? Iss{ get; set; }

    }
}
