using Microsoft.AspNetCore.Mvc;
using System;

namespace CustomerRegisterTextfile.Controllers
{
    [Route("Demo")]
    public class DemoController : Controller
    {
        [HttpGet, Route("SquareRoot")]
        public IActionResult SquareRoot(double? number)
        {
            if (number == null)
            {
                return BadRequest("Mata in ett riktigt tal!");
            }

            if (number < 0)
            {
                return BadRequest("Det går inte att ta roten ur negativa tal");
            }
            var result = Math.Sqrt((double)number);

            return Ok(result);
        }
    }
}
