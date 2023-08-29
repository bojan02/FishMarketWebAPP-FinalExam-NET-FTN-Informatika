using System.ComponentModel.DataAnnotations;

namespace ZavrsniIspit.Models
{
    public class Ribarnica
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Naziv { get; set; }
        [Required]
        [Range(1950, 2022)]
        public int GodinaOtvaranja { get; set; }
    }
}
