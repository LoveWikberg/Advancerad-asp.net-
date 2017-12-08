using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LabbTvå.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet, Route("breakfast")]
        public IActionResult Breakfast(string toEat)
        {
            if (toEat == "ägg")
                return Ok("Å nej, du borde inte äta ägg till frukost!");
            else
                return Ok($"Ja {toEat} är fan gött");
        }

        [HttpGet, Route("square")]
        public IActionResult Square(int numberToSquare)
        {
            return Ok(numberToSquare * numberToSquare);
        }
        [HttpGet, Route("listnumbers")]
        public IActionResult ListNumbers(int lowest, int highest)
        {
            if (highest <= lowest)
                return Ok("Fel");
            else
                return Ok(Enumerable.Range(lowest, highest - (lowest - 1)).ToArray());
        }

        [HttpGet, Route("changebackground")]
        public IActionResult ChangeBackground(string color)
        {
            return Content($"<body style='background-color:{color}' ></body>", "text/html");
        }
    }
}
