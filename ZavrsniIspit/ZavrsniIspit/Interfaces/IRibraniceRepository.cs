using System.Collections.Generic;
using System.Linq;
using ZavrsniIspit.Models;

namespace ZavrsniIspit.Interfaces
{
    public interface IRibraniceRepository
    {
        IQueryable<Ribarnica> GetAll();
        Ribarnica GetById(int id);
        IQueryable<Ribarnica> GetRibartniceByNaziv(string naziv);


    }
}
