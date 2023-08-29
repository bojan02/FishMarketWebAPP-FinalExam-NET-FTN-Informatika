using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ZavrsniIspit.Interfaces;
using ZavrsniIspit.Models;

namespace ZavrsniIspit.Repository
{
    public class RibaRepository : IRibaRepository
    {
        private readonly AppDbContext _context;

        public RibaRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Riba> GetAll()
        {
            return _context.Ribe.OrderBy(f => f.Sorta);
        }

        public Riba GetById(int id)
        {
            return _context.Ribe.Include(r => r.Ribarnica).FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<StarostRibarniceProsecnaCenaRibaDTO> GetInfoRibarnice(double granica)
        {
            return _context.Ribe.GroupBy(r => r.Ribarnica.Id)
                .Select(r => new StarostRibarniceProsecnaCenaRibaDTO
                {
                    Ribarnica = _context.Ribarnice.Where(a => a.Id == r.Key).Select(a => a.Naziv).Single(),
                    StarostRibarnica = DateTime.Now.Year - _context.Ribarnice.Where(a => a.Id == r.Key).Select(a => a.GodinaOtvaranja).Single(),
                    ProsecnaCenaRibaProdavnice = r.Average(a => a.Cena)

                }).Where(a => a.ProsecnaCenaRibaProdavnice < granica).OrderByDescending(a => a.Ribarnica);


        }

        //Koristi .Distinct().Count() ako se ribe pojavlju dva puta sa istim imenom u jednoj ribarnici
        public IQueryable<NazivRibarniceBrojRibaUkKolicina> GetStanjeRibarnice()
        {
            return _context.Ribe.GroupBy(r => r.Ribarnica.Id)
                 .Select(r => new NazivRibarniceBrojRibaUkKolicina
                 {
                     Ribarnica = _context.Ribarnice.Where(a => a.Id == r.Key).Select(a => a.Naziv).Single(),
                     BrojRazlicitihRiba = r.Select(a => a.Sorta).Distinct().Count(),
                     UkupnaDostupnaKolicina = r.Sum(a => a.DostupnaKolicina)


                 }).OrderByDescending(a => a.BrojRazlicitihRiba);
        }
        
        public void Add(Riba riba)
        {
            _context.Ribe.Add(riba);
            _context.SaveChanges();
        }

        public void Update(Riba riba)
        {
            _context.Entry(riba).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Riba riba)
        {
            _context.Ribe.Remove(riba);
            _context.SaveChanges();
        }



        IQueryable<Riba> IRibaRepository.GetRibeBySorta(string sorta)
        {
            return _context.Ribe.Where(a => a.Sorta.Equals(sorta)).OrderBy(c => c.Cena);

        }

        public IQueryable<Riba> GetAllByParameters(int najmanje, int najvise)
        {
            return _context.Ribe.Include(c => c.Ribarnica).Where(c => c.DostupnaKolicina >= najmanje && c.DostupnaKolicina <= najvise).OrderByDescending(c => c.Cena);
        }

        
    }
}
