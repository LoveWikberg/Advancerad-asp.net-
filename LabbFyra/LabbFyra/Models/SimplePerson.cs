using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabbFyra.Models
{
    public class SimplePerson
    {
        [Required(ErrorMessage = "Fyll i namn")]
        [MaxLength(20, ErrorMessage = "För långt namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fyll i ålder")]
        [Range(0, 120, ErrorMessage = "Felaktig ålder")]
        public int ?Age { get; set; }
    }
}
