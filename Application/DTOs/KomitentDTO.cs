using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class KomitentDTO
    {
        [Required]
        public string Naziv { get; set; } = string.Empty;
        [Required]
        public string Adresa { get; set; } = string.Empty;
    }
}
