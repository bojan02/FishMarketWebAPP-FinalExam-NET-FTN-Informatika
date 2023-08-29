using System.Linq;
using ZavrsniIspit.Models;

namespace ZavrsniIspit.Interfaces
{
    public interface IRibaRepository
    {
        IQueryable<StarostRibarniceProsecnaCenaRibaDTO> GetInfoRibarnice(double granica);
        IQueryable<NazivRibarniceBrojRibaUkKolicina> GetStanjeRibarnice();
        IQueryable<Riba> GetAll();
        Riba GetById(int id);
        IQueryable<Riba> GetRibeBySorta(string sorta);
        void Add(Riba riba);
        void Update(Riba riba);
        void Delete(Riba riba);
        IQueryable<Riba> GetAllByParameters(int najmanje, int najvise);
    }
}
