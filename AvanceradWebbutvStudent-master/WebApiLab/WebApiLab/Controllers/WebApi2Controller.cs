using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiLab.Controllers
{
    [Route("webapi2")]
    public class WebApi2Controller : Controller
    {

        [HttpGet, Route("Breakfast")]
        public IActionResult Breakfast(string food)
        {
            if (food.ToLower() == "ägg")
            {
                return Ok("Å nej, du borde inte äta ägg till frukost!");
            }

            string message = $"Ja, {food} är gott!";

            return Ok(message);
        }

        [HttpGet, Route("Square")]
        public IActionResult Square(int number)
        {
            int numberedSquared = number * number;
            string message = $"{number} * {number} = {numberedSquared}";
            return Ok(message);
        }

        [HttpGet, Route("Range")]
        public IActionResult Range(int from, int to)
        {
            var numbers = new List<int>();

            for (int i = from; i <= to; i++)
            {
                numbers.Add(i);
            }

            string numbersAsString = string.Join(",", numbers);

            return Ok(numbersAsString);
        }

        [HttpGet, Route("SetBackground")]
        public IActionResult SetBackground(string color)
        {
            string html = $"<body style='background-color:{color}'></body>";
            return Content(html, "text/html");
        }



    }
}