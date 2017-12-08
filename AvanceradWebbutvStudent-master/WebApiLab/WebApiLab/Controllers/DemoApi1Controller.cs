using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiLab.Controllers
{
    [Route("demo1")]
    public class DemoApi1Controller : Controller
    {

        [HttpGet, Route("GetPainting")]
        public IActionResult GetPainting(string painter, string paintedDate)
        {
            string response = $"Du angav konstnären {painter} och datumet {paintedDate}";
            return Ok(response);
        }

    }
}