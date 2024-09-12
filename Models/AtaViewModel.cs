namespace iştakip.Models
{
    public class AtaViewModel
    {
        public string IsBaslık { get; set; }        // Başlık alanı
        public string IsAcıklama { get; set; }      // Açıklama alanı
        public int SelectedPersonelId { get; set; }
        public int IsDurumId { get; set; }
        public DateTime BaslangicTarih { get; set; } // Added field for start date
        public DateTime BitisTarih { get; set; } // Added field for end date
    }
}
