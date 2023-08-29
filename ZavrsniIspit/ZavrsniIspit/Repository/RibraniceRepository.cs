using System.Linq;
using ZavrsniIspit.Interfaces;
using ZavrsniIspit.Models;

namespace ZavrsniIspit.Repository
{
    public class RibraniceRepository : IRibraniceRepository
    {
        private readonly AppDbContext _context;

        public RibraniceRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Ribarnica> GetAll()
        {
            return _context.Ribarnice.OrderBy(c => c.Naziv);
        }

        public Ribarnica GetById(int id)
        {
            return _context.Ribarnice.FirstOrDefault(p => p.Id == id); ;
        }

        public IQueryable<Ribarnica> GetRibartniceByNaziv(string naziv)
        {
            return _context.Ribarnice.Where(a => a.Naziv.Contains(naziv)).OrderBy(c => c.GodinaOtvaranja).ThenByDescending(a => a.Naziv);
        }


    }
}
