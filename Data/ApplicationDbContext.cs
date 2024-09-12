using Microsoft.EntityFrameworkCore;
using iştakip.Models;


namespace iştakip.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }
        public DbSet<Personel>? Personels { get; set; }
        public DbSet<Birim>? Birims { get; set; }
        public DbSet<YetkiTur>? YetkiTurs { get; set; }
        public DbSet<Is>? Iss { get; set; }
        public DbSet<Durum>? Durums{ get; set; }
        
    }
}
