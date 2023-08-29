using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ZavrsniIspit.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ribarnica> Ribarnice { get; set; }
        public DbSet<Riba> Ribe { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ribarnica>().HasData(
                new Ribarnica() { Id = 1, Naziv = "Dunav doo", GodinaOtvaranja = 2015 },
                new Ribarnica() { Id = 2, Naziv = "Tisa str", GodinaOtvaranja = 2012 },
                new Ribarnica() { Id = 3, Naziv = "Sveza riba", GodinaOtvaranja = 2015 }
            );

            modelBuilder.Entity<Riba>().HasData(
                new Riba()
                {
                    Id = 1,
                    Sorta = "Smudj",
                    MestoUlova = "Ribnjak Bager",
                    Cena = 1100,
                    DostupnaKolicina = 20,
                    RibarnicaId = 3
                },
               new Riba()
               {
                   Id = 2,
                   Sorta = "Saran",
                   MestoUlova = "Dunav",
                   Cena = 860,
                   DostupnaKolicina = 30,
                   RibarnicaId = 1
               },
                new Riba()
                {
                    Id = 3,
                    Sorta = "Som",
                    MestoUlova = "Tisa",
                    Cena = 1300,
                    DostupnaKolicina = 10,
                    RibarnicaId = 2
                },
                new Riba()
                {
                    Id = 4,
                    Sorta = "Saran",
                    MestoUlova = "Ribnjak Ecka",
                    Cena = 780,
                    DostupnaKolicina = 12,
                    RibarnicaId = 3
                },
                new Riba()
                {
                    Id = 5,
                    Sorta = "Smudj",
                    MestoUlova = "Dunav",
                    Cena = 950,
                    DostupnaKolicina = 15,
                    RibarnicaId = 1
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
