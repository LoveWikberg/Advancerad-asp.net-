using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{
    [Route("webapi1")]
    public class WebApi1Controller : Controller
    {
        [HttpGet, Route("HelloWorld")]
        public string HelloWorld()
        {
            return "Hello world";
        }

        [HttpGet, Route("Number")]
        public int Number()
        {
            return 42;
        }

        [HttpGet, Route("HelloWorld2")]
        public IActionResult HelloWorld2()
        {
            return Ok("Hello world - with IActionResult");
        }

        [HttpGet, Route("Weekday")]
        public IActionResult Weekday()
        {
            return Ok($"Today it is {DateTime.Now.DayOfWeek}");
        }

        [HttpGet, Route("ClicheOfTheDay")]
        public IActionResult ClicheOfTheDay()
        {
            string message = "";
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday: message = "Uh-oh. Sounds like somebody's got a case of the mondays."; break;
                case DayOfWeek.Tuesday: message = "Tuesday text"; break;
                case DayOfWeek.Wednesday: message = "Wednesday text"; break;
                case DayOfWeek.Thursday: message = "Thursday text"; break;
                case DayOfWeek.Friday: message = "Friday text"; break;
                case DayOfWeek.Saturday: message = "Saturday text"; break;
                case DayOfWeek.Sunday: message = "Sunday text"; break;
                default: throw new Exception($"Don't recognize weekday {DateTime.Now.DayOfWeek}");
            }

            return Ok(message);
        }

    }
}
