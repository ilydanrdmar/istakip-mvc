using System.ComponentModel.DataAnnotations;

namespace iştakip.Models
{
    public class Is
    {
        [Key]
        public int IsId { get; set; }
        public string? IsBaslık { get; set; }
        public string? IsAcıklama { get; set; }

        public int? IsPersonelId{ get; set; }
        public  Personel? Personel { get; set; }

        public string? PersonelBirimId { get; set; }
        public DateTime? BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }
        public int? IsDurumId { get; set; }
        public  Durum? Durum { get; set; }


    }
}
