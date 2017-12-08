using System.ComponentModel.DataAnnotations;

namespace WebApiLab.Models
{
    public class SimplePersonWithAttributes
    {
        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Förnamnet får inte vara tomt")]
        [StringLength(20, ErrorMessage = "Förnamnet får inte innehålla fler än 20 tecken")]
        public string FirstName { get; set; }

        [Display(Name = "Ålder")]
        [Required(ErrorMessage = "Ålder får inte vara tomt")]
        [Range(0, 120, ErrorMessage = "Ålder måste vara mellan 0 och 120")]
        public int? Age { get; set; }
    }
}
