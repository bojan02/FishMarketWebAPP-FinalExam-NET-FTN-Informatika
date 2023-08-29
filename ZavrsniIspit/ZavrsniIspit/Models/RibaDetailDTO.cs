using System.ComponentModel.DataAnnotations;

namespace ZavrsniIspit.Models
{
    public class RibaDetailDTO
    {
        public int Id { get; set; }
        public string Sorta { get; set; }
        public string MestoUlova { get; set; }
        public double Cena { get; set; }
        public int DostupnaKolicina { get; set; }
        public int RibarnicaId { get; set; }
        public string RibarnicaNaziv { get; set; }
        public int RibarnicaGodinaOtvaranja { get; set; }

    }
}
