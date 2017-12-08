using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LabbFyra.Models;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LabbFyra.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        [HttpPost, Route("addperson")]
        public IActionResult AddPerson(SimplePerson smpPerson)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (smpPerson.Age == null || !Enumerable.Range(0, 120).Any(n => n == smpPerson.Age))
                strBuilder.Append("Felaktig ålder\n");
            if (smpPerson.Name == null || smpPerson.Name.Length > 20)
                strBuilder.Append("Felaktigt namn");
            if (strBuilder.Length != 0)
                return BadRequest(strBuilder.ToString());
            return Ok($"Du har angett {smpPerson.Name} som är {smpPerson.Age} år gammal.");
        }

        [HttpPost, Route("addpersonwithattribute")]
        public IActionResult AddPersonWithAttribute(SimplePerson smpPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage).Reverse());
            }
            return Ok($"Du har angett {smpPerson.Name} som är {smpPerson.Age} år gammal.");
        }
    }
}
