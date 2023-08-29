using System;
using System.ComponentModel.DataAnnotations;

namespace ZavrsniIspit.Models
{
    public class Riba
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Sorta { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string MestoUlova { get; set; }
        [Required]
        [Range(100, 10000)]
        public double Cena { get; set; }
        [Required]
        [Range(1, 1000)]
        public int DostupnaKolicina { get; set; }
        public Ribarnica Ribarnica { get; set; }
        public int RibarnicaId { get; set; }
    }
}
