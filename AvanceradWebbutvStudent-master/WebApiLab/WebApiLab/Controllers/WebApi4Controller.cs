using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using WebApiLab.Models;

namespace WebApiLab.Controllers
{
    [Route("webapi4")]
    public class WebApi4Controller : Controller
    {
        [HttpPost, Route("AddPerson")]
        public IActionResult AddPerson(SimplePerson person)
        {
            string message = $"Du har angett {person.FirstName} som är {person.Age} år";
            return Ok(message);
        }

        [HttpPost, Route("AddPersonValidate")]
        public IActionResult AddPersonValidate(SimplePerson person)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(person.FirstName))
            {
                errors.Add("Förnamnet får inte vara tomt");
            }

            if (person.FirstName?.Length > 20)
            {
                errors.Add("Förnamnet får inte innehålla fler än 20 tecken");
            }

            if (person.Age == null)
            {
                errors.Add("Ålder måste anges");
            }

            if (person.Age < 0 || person.Age > 120)
            {
                errors.Add("Ålder måste vara mellan 0 och 120");
            }

            if (errors.Count > 0)
            {
                return BadRequest(string.Join(". ", errors));
            }
            else
            {
                return Ok($"Du har angett {person.FirstName} som är {person.Age} år");
            }

        }

        [HttpPost, Route("AddPersonValidateAttribute")]
        public IActionResult AddPersonValidateAttribute(SimplePersonWithAttributes person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                //var messages = new List<string>();

                //if (ModelState["FirstName"].ValidationState != ModelValidationState.Valid)
                //{
                //    string err = ModelState["FirstName"].Errors[0].ErrorMessage;
                //    messages.Add(err);
                //}

                //if (ModelState["Age"].ValidationState != ModelValidationState.Valid)
                //{
                //    string err = ModelState["Age"].Errors[0].ErrorMessage;
                //    messages.Add(err);
                //}
                //return BadRequest(string.Join(". ", messages));

            }

            return Ok($"Du har angett {person.FirstName} som är {person.Age} år");
        }
    }
}